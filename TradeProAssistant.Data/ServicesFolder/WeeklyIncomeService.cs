﻿using Data.Framework;
using Entities;
using HtmlBuilders;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TradeProAssistant.Data.Framework;
using System.Linq;
using Enums;

namespace Services
{
    public class WeeklyIncomeService : ServiceBase, IDisposable
    {
        #region Properties
        public String JobId { get; set; }
        Random random = new Random(DateTime.Now.Millisecond);
        #endregion

        #region Constructors
        public WeeklyIncomeService()
        {

        }

        public WeeklyIncomeService(String jobId)
        {
            this.JobId = jobId;
        }
        #endregion

        #region PerformWeeklyIncomeActions
        public async Task PerformWeeklyIncomeActions(int slots, Decimal minStrikeDiff, Decimal maxRisk, int strikePadding, List<int> actions)
        {
            foreach(int action in actions.OrderBy(x => x))
            {
                switch ((WeeklyIncomeActions)action)
                {
                    case WeeklyIncomeActions.SetImportantDates:
                        using (SecurityService service = new SecurityService(this.JobId))
                        {
                            service.ProgressMessageRaised += SecurityService_ProgressMessageRaised;

                            await service.ScrapeDatesMaster();
                        }
                        break;
                    case WeeklyIncomeActions.DownloadOptionChains:
                        await DownloadOptionChains();
                        break;
                    case WeeklyIncomeActions.GeneratePairCondorPlaySheet:
                        await BuildPcPlan(slots, minStrikeDiff, maxRisk, strikePadding);
                        break;
                    case WeeklyIncomeActions.GenerateIronCondorPlaySheet:
                        await BuildIcPlan(slots, minStrikeDiff, maxRisk, strikePadding);
                        break;
                    case WeeklyIncomeActions.SetBenzingaIds:
                        using (SecurityService service = new SecurityService(this.JobId))
                        {
                            service.ProgressMessageRaised += SecurityService_ProgressMessageRaised;

                            await service.ScrapeBenzingaIdsMaster();
                        }
                        break;
                }
            }
        }
        #endregion

        #region Pair Condor Methods
        #region BuildPcPlan
        public async Task BuildPcPlan(int slots, Decimal minStrikeDiff, Decimal maxRisk, int strikePadding)
        {
            Thread.Sleep(1000);

            Query query = new Query();
            query.SortPropertyName = Security.PropertyNames.Symbol;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.PairEligible,
                Parameter = "true",
                QueryOperator = QueryOperators.Equals,
                IsAndFilter = true
            });

            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.Ignore,
                Parameter = "false",
                QueryOperator = QueryOperators.Equals,
                IsAndFilter = true
            });

            query.Includes.Add(Security.PropertyNames.OptionChainsInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.DatesInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.Dates.StrikesInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.Dates.Strikes.CallInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.Dates.Strikes.PutInclude);

            List<Security> securities = SecurityService.GetCollection(query);
            securities.FilterForImportantDates();

            //securities = securities.Where(x => x.SectorEnum == Enums.Sectors.Index).ToList();

            OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item").Class("active")
                        .Append(HtmlTags.B.Append(securities.Count.ToString()))
                        .Append(HtmlTags.Span.Append("&nbsp;optionable securities retrieved"))
                        .ToHtml().ToString(), this.JobId);

            foreach (Security security in securities)
            {
                security.MinStrikeDiff = minStrikeDiff;
                security.MaxRisk = maxRisk;
                security.StrikePadding = strikePadding;

                OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
                .Append(HtmlTags.B.Append($"{security.Symbol}"))
                .Append(HtmlTags.Span.Append("&nbsp;expected move:&nbsp;"))
                .Append(HtmlTags.B.Append($"{security.ExpectedMove:C} ({(security.ExpectedMove / security.CurrentPrice):P2})"))
                .Append(HtmlTags.Span.Append("&nbsp;credit spreads:&nbsp;"))
                .Append(HtmlTags.B.Append($"{security.PcBearCallSpread}, {security.PcBullPutSpread}"))
                //.Append(HtmlTags.Span.Append($",&nbsp;price range:&nbsp;"))
                //.Append(HtmlTags.B.Append($"{security.LowerBoundStrike:C} : {security.ExpectedLowerMove:C} : {security.CurrentPrice:C} : {security.ExpectedUpperMove:C} : {security.UpperBoundStrike:C}"))
                //.Append(HtmlTags.Span.Append($",&nbsp;step/qty:&nbsp;"))
                //.Append(HtmlTags.B.Append($"{security.StrikeStep:C} / {security.ContractQuantity}"))
                .ToHtml().ToString(), this.JobId);
            }

            WeeklyIncomePlaySheet playSheet = new WeeklyIncomePlaySheet();
            playSheet.TimeStamp = DateTime.Now;
            playSheet.Expiry = securities[0].LatestOptionDate.ExpiryDate;

            //var results = from p in persons
            //              group p.car by p.PersonId into g
            //              select new { PersonId = g.Key, Cars = g.ToList() };

            var sectorGroups = from s in securities
                               group s by s.SectorEnum into g
                               select new { Sector = g.Key, Securities = g.ToList() };

            List<PairCondor> pairCondorCandidates = new List<PairCondor>();

            foreach (var sectorGroup in sectorGroups)
            {
                OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
               .Append(HtmlTags.Span.Append("Sector group:&nbsp;"))
               .Append(HtmlTags.B.Append($"{sectorGroup.Sector.GetStringValue()}"))
               .ToHtml().ToString(), this.JobId);

                if (sectorGroup.Securities.Count > 1)
                {
                    for (int i = 0; i < sectorGroup.Securities.Count - 1; i++)
                    {
                        for (int j = i + 1; j < sectorGroup.Securities.Count; j++)
                        {
                            Security iSecurity = sectorGroup.Securities[i];
                            Security jSecurity = sectorGroup.Securities[j];

                            if (!iSecurity.IsBearish && !jSecurity.IsBullish)
                            {
                                PairCondor ijPairCondor = new PairCondor();
                                ijPairCondor.SectorEnum = sectorGroup.Sector;
                                ijPairCondor.Strategy = StrategyTypes.PairCondor;
                                ijPairCondor.BullPutSpread = iSecurity.PcBullPutSpread;
                                ijPairCondor.BearCallSpread = jSecurity.PcBearCallSpread;
                                pairCondorCandidates.Add(ijPairCondor);
                            }

                            if (!jSecurity.IsBearish && !iSecurity.IsBullish)
                            {
                                PairCondor jiPairCondor = new PairCondor();
                                jiPairCondor.SectorEnum = sectorGroup.Sector;
                                jiPairCondor.Strategy = StrategyTypes.PairCondor;
                                jiPairCondor.BullPutSpread = jSecurity.PcBullPutSpread;
                                jiPairCondor.BearCallSpread = iSecurity.PcBearCallSpread;
                                pairCondorCandidates.Add(jiPairCondor);
                            }
                        }
                    }
                }
            }

            List<WeeklyIncomeComboCount> comboCounts = (from x in pairCondorCandidates
                                                        group x by x.SectorEnum into g
                                                        select new WeeklyIncomeComboCount()
                                                        {
                                                            SectorEnum = g.Key,
                                                            Count = g.Count()
                                                        }).OrderByDescending(x => x.Count).ToList();

            playSheet.ComboCounts = comboCounts;
            playSheet.Strategy = StrategyTypes.PairCondor;

            int playSheetId = WeeklyIncomePlaySheetService.Save(playSheet);
            pairCondorCandidates = pairCondorCandidates.OrderByDescending(x => x.Credit).Select(x => new PairCondor(x)).ToList();
            WeeklyIncomeActionPlan bruteForceActionPlan = PcBruteForceActionPlanGenerator(pairCondorCandidates, slots);
            bruteForceActionPlan.PlaySheetIdentifier = playSheetId;
            WeeklyIncomeActionPlanService.Save(bruteForceActionPlan);

            pairCondorCandidates = pairCondorCandidates.OrderByDescending(x => x.Credit).Select(x => new PairCondor(x)).ToList();
            WeeklyIncomeActionPlan randomSearchActionPlan = PcRandomSearchActionPlanGenerator(pairCondorCandidates, slots);
            randomSearchActionPlan.PlaySheetIdentifier = playSheetId;
            WeeklyIncomeActionPlanService.Save(randomSearchActionPlan);

            pairCondorCandidates = pairCondorCandidates.OrderByDescending(x => x.Credit).Select(x => new PairCondor(x)).ToList();
            WeeklyIncomeActionPlan geneticOptimizationActionPlan = PcGeneticOptimizationActionPlanGenerator(pairCondorCandidates, slots);
            geneticOptimizationActionPlan.PlaySheetIdentifier = playSheetId;
            WeeklyIncomeActionPlanService.Save(geneticOptimizationActionPlan);

            Thread.Sleep(1000);
            OnRedirectRaised("WeeklyIncome", "PlaySheet", playSheetId, this.JobId);
        }
        #endregion

        #region PcActionPlanGenerators
        #region PcBruteForceActionPlanGenerator
        private WeeklyIncomeActionPlan PcBruteForceActionPlanGenerator(List<PairCondor> pairCondorCandidates, int slots)
        {
            WeeklyIncomeActionPlan actionPlan = new WeeklyIncomeActionPlan();
            actionPlan.GenerationMethod = Enums.WeeklyActionPlanGenerationMethods.BruteForce;

            foreach (PairCondor pairCondor in pairCondorCandidates.OrderByDescending(x => x.Credit))
            {
                actionPlan.AddPairCondor(pairCondor);

                if (actionPlan.Pairs.Count == slots)
                {
                    break;
                }
            }

            return actionPlan;
        }
        #endregion

        #region PcRandomSearchActionPlanGenerator
        private WeeklyIncomeActionPlan PcRandomSearchActionPlanGenerator(List<PairCondor> pairCondorCandidates,
            int slots, int iterations = 500)
        {
            WeeklyIncomeActionPlan best = BuildRandomSolution(pairCondorCandidates, WeeklyActionPlanGenerationMethods.RandomSearch, slots);

            for (int i = 0; i < iterations; i++)
            {
                WeeklyIncomeActionPlan temp = BuildRandomSolution(pairCondorCandidates, WeeklyActionPlanGenerationMethods.RandomSearch, slots);
                if (temp.Credit > best.Credit)
                {
                    best = temp;
                }
            }

            return best;
        }
        #endregion

        #region PcGeneticOptimizationActionPlanGenerator
        private WeeklyIncomeActionPlan PcGeneticOptimizationActionPlanGenerator(List<PairCondor> pairCondorCandidates,
            int slots, int popSize = 50, Double mutProb = .2, Double elite = .2, int maxIter = 100)
        {
            List<WeeklyIncomeActionPlan> pop = new List<WeeklyIncomeActionPlan>();
            for (int i = 0; i < popSize; i++)
            {
                pop.Add(BuildRandomSolution(pairCondorCandidates, WeeklyActionPlanGenerationMethods.GeneticOptimization, slots));
            }

            int topElite = (int)(elite * popSize);

            for (int i = 0; i < maxIter; i++)
            {
                pop = pop.OrderByDescending(x => x.Credit).Take(topElite).ToList();

                while (pop.Count < popSize)
                {
                    if (random.NextDouble() < mutProb)
                    {
                        WeeklyIncomeActionPlan mut = new WeeklyIncomeActionPlan(pop.Take(topElite).ToList()[random.Next(0, topElite)]);
                        Mutate(mut, pairCondorCandidates, slots);
                        pop.Add(mut);
                    }
                    else
                    {
                        WeeklyIncomeActionPlan r1 = new WeeklyIncomeActionPlan(pop.Take(topElite).ToList()[random.Next(0, topElite)]);
                        WeeklyIncomeActionPlan r2 = new WeeklyIncomeActionPlan(pop.Take(topElite).ToList()[random.Next(0, topElite)]);
                        pop.Add(Crossover(r1, r2, pairCondorCandidates, slots));
                    }
                }
            }

            return pop.OrderByDescending(x => x.Credit).First();
        }
        #endregion

        #region Helper Methods
        #region BuildRandomSolution
        private WeeklyIncomeActionPlan BuildRandomSolution(List<PairCondor> pairCondorCandidates, WeeklyActionPlanGenerationMethods generationMethod, int slots)
        {
            WeeklyIncomeActionPlan actionPlan = new WeeklyIncomeActionPlan();
            actionPlan.GenerationMethod = generationMethod;

            while (actionPlan.Pairs.Count < slots)
            {
                int idx = random.Next(pairCondorCandidates.Count);
                actionPlan.AddPairCondor(pairCondorCandidates[idx]);
            }

            return actionPlan;
        }
        #endregion

        #region Mutate
        private void Mutate(WeeklyIncomeActionPlan actionPlan, List<PairCondor> pairCondorCandidates, int slots)
        {
            int step = random.Next(0, 2) == 0 ? 1 : -1;
            int rmvIdx = random.Next(0, actionPlan.Pairs.Count);
            PairCondor rmvPair = actionPlan.Pairs[rmvIdx];
            int pairIdx = pairCondorCandidates.FindIndex(x => x.BullPutSpread.SecurityIdentifier == rmvPair.BullPutSpread.SecurityIdentifier
                && x.BearCallSpread.SecurityIdentifier == rmvPair.BearCallSpread.SecurityIdentifier);

            actionPlan.Pairs.RemoveAt(rmvIdx);

            pairIdx = MutateIndex(pairIdx, step, pairCondorCandidates);
            while (actionPlan.Pairs.Count < slots)
            {
                actionPlan.AddPairCondor(pairCondorCandidates[pairIdx]);
                pairIdx = MutateIndex(pairIdx, step, pairCondorCandidates);
            }
        }

        private int MutateIndex(int pairIdx, int step, List<PairCondor> pairCondorCandidates)
        {
            pairIdx += step;

            if (pairIdx < 0)
            {
                pairIdx = pairCondorCandidates.Count - 1;
            }
            else if (pairIdx > (pairCondorCandidates.Count - 1))
            {
                pairIdx = 0;
            }

            return pairIdx;
        }
        #endregion

        #region Crossover
        private WeeklyIncomeActionPlan Crossover(WeeklyIncomeActionPlan r1, WeeklyIncomeActionPlan r2, List<PairCondor> pairCondorCandidates, int slots)
        {
            WeeklyIncomeActionPlan co = new WeeklyIncomeActionPlan();
            co.GenerationMethod = r1.GenerationMethod;

            int splitIdx = random.Next(1, slots - 1);
            co.Pairs.AddRange(r1.Pairs.Take(splitIdx));

            for (int i = splitIdx; i < r2.Pairs.Count; i++)
            {
                co.AddPairCondor(r2.Pairs[splitIdx]);
            }

            while (co.Pairs.Count < slots)
            {
                int idx = random.Next(pairCondorCandidates.Count);
                co.AddPairCondor(pairCondorCandidates[idx]);
            }

            return co;
        }
        #endregion

        #endregion
        #endregion
        #endregion

        #region Iron Condor Methods
        #region BuildIcPlan
        public async Task BuildIcPlan(int slots, Decimal minStrikeDiff, Decimal maxRisk, int strikePadding)
        {
            Thread.Sleep(1000);

            Query query = new Query();
            query.SortPropertyName = Security.PropertyNames.Symbol;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.IronCondorEligible,
                Parameter = "true",
                QueryOperator = QueryOperators.Equals,
                IsAndFilter = true
            });

            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.Ignore,
                Parameter = "false",
                QueryOperator = QueryOperators.Equals,
                IsAndFilter = true
            });

            query.Includes.Add(Security.PropertyNames.OptionChainsInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.DatesInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.Dates.StrikesInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.Dates.Strikes.CallInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.Dates.Strikes.PutInclude);

            List<Security> securities = SecurityService.GetCollection(query);
            securities.FilterForImportantDates();

            //securities = securities.Where(x => x.SectorEnum == Enums.Sectors.Index).ToList();

            OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item").Class("active")
                        .Append(HtmlTags.B.Append(securities.Count.ToString()))
                        .Append(HtmlTags.Span.Append("&nbsp;optionable securities retrieved"))
                        .ToHtml().ToString(), this.JobId);


            WeeklyIncomePlaySheet playSheet = new WeeklyIncomePlaySheet();
            playSheet.TimeStamp = DateTime.Now;
            playSheet.Expiry = securities[0].LatestOptionDate.ExpiryDate;

            List<PairCondor> ironCondorCandidates = new List<PairCondor>();

            foreach (Security security in securities)
            {
                security.MinStrikeDiff = minStrikeDiff;
                security.MaxRisk = maxRisk;
                security.StrikePadding = strikePadding;
            }

            foreach (Security security in securities.Where(x => x.IcOptionsExist))
            {
                PairCondor ironCondor = new PairCondor();
                ironCondor.SectorEnum = security.SectorEnum;
                ironCondor.Strategy = StrategyTypes.IronCondor;
                ironCondor.BullPutSpread = security.IcBullPutSpread;
                ironCondor.BearCallSpread = security.IcBearCallSpread;
                ironCondorCandidates.Add(ironCondor);
            }

            playSheet.Strategy = StrategyTypes.IronCondor;

            int playSheetId = WeeklyIncomePlaySheetService.Save(playSheet);
            ironCondorCandidates = ironCondorCandidates.OrderByDescending(x => x.Credit).Select(x => new PairCondor(x)).ToList();
            WeeklyIncomeActionPlan bruteForceActionPlan = IcBruteForceActionPlanGenerator(ironCondorCandidates, slots);
            bruteForceActionPlan.PlaySheetIdentifier = playSheetId;
            WeeklyIncomeActionPlanService.Save(bruteForceActionPlan);

            Thread.Sleep(1000);
            OnRedirectRaised("WeeklyIncome", "PlaySheet", playSheetId, this.JobId);
        }
        #endregion 

        #region IcBruteForceActionPlanGenerator
        private WeeklyIncomeActionPlan IcBruteForceActionPlanGenerator(List<PairCondor> ironCondorCandidates, int slots)
        {
            WeeklyIncomeActionPlan actionPlan = new WeeklyIncomeActionPlan();
            actionPlan.GenerationMethod = Enums.WeeklyActionPlanGenerationMethods.BruteForce;

            foreach (PairCondor ironCondor in ironCondorCandidates.OrderByDescending(x => x.Credit))
            {
                actionPlan.Pairs.Add(ironCondor);

                if (actionPlan.Pairs.Count == slots)
                {
                    break;
                }
            }

            return actionPlan;
        }
        #endregion
        #endregion

        #region DownloadOptionChains
        public async Task DownloadOptionChains()
        {
            Thread.Sleep(1000);

            DateTime monday = DateTime.Now.GetPreviousDayOfWeekOccurrence(DayOfWeek.Monday);
            DateTime friday = DateTime.Now.GetPreviousDayOfWeekOccurrence(DayOfWeek.Friday);

            Query query = new Query();
            query.SortPropertyName = Security.PropertyNames.Symbol;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.PairEligible,
                Parameter = "true",
                QueryOperator = QueryOperators.Equals
            });

            List<Security> securities = SecurityService.GetCollection(query);

            securities.RemoveAll(x => (x.NextEarningsDate.HasValue && x.NextEarningsDate >= monday && x.NextEarningsDate <= friday));
            securities.RemoveAll(x => (x.ExDividendDate.HasValue && x.ExDividendDate >= monday && x.ExDividendDate <= friday));

            for (int i = 0; i < securities.Count; i++)
            {
                Security security = securities[i];

                OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item").Class("active")
                        .Append(HtmlTags.Span.Append("Processing security:&nbsp;"))
                        .Append(HtmlTags.B.Append(security.Symbol)).ToHtml().ToString(), this.JobId);

                if (SecurityService.GetCurrentPrice(security))
                {
                    SecurityService.Save(security);

                    OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
                        .Append(HtmlTags.B.Append(security.Symbol))
                        .Append(HtmlTags.Span.Append($"&nbsp;current price: {security.CurrentPrice:C}"))
                        .ToHtml().ToString(), this.JobId);

                    OptionChain optionChain = OptionChainService.GetOpcOptionChain(security);
                    if (optionChain != null)
                    {
                        OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
                        .Append(HtmlTags.B.Append(security.Symbol))
                        .Append(HtmlTags.Span.Append("&nbsp;option chain retrieved"))
                        .ToHtml().ToString(), this.JobId);

                        optionChain.SecurityIdentifier = security.Identifier;

                        OptionChainService.Save(optionChain);
                    }
                }
            }
        }
        #endregion

        #region Purge
        public void Purge()
        {
            Thread.Sleep(1500);

            SendPurgeMessage("WeeklyIncomeComboCount");
            foreach (WeeklyIncomeComboCount x in WeeklyIncomeComboCountService.GetCollection())
            {
                WeeklyIncomeComboCountService.Delete(x);
            }

            SendPurgeMessage("OptionStrike");
            foreach (OptionStrike x in OptionStrikeService.GetCollection())
            {
                OptionStrikeService.Delete(x);
            }

            SendPurgeMessage("OptionDate");
            foreach (OptionDate x in OptionDateService.GetCollection())
            {
                OptionDateService.Delete(x);
            }

            SendPurgeMessage("OptionChain");
            foreach (OptionChain x in OptionChainService.GetCollection())
            {
                OptionChainService.Delete(x);
            }

            SendPurgeMessage("BullPutSpread");
            foreach (BullPutSpread x in BullPutSpreadService.GetCollection())
            {
                BullPutSpreadService.Delete(x);
            }

            SendPurgeMessage("BearCallSpread");
            foreach (BearCallSpread x in BearCallSpreadService.GetCollection())
            {
                BearCallSpreadService.Delete(x);
            }

            SendPurgeMessage("PairCondor");
            foreach (PairCondor x in PairCondorService.GetCollection())
            {
                PairCondorService.Delete(x);
            }

            SendPurgeMessage("WeeklyIncomeActionPlan");
            foreach (WeeklyIncomeActionPlan x in WeeklyIncomeActionPlanService.GetCollection())
            {
                WeeklyIncomeActionPlanService.Delete(x);
            }

            SendPurgeMessage("Call");
            foreach (Call x in CallService.GetCollection())
            {
                CallService.Delete(x);
            }

            SendPurgeMessage("Put");
            foreach (Put x in PutService.GetCollection())
            {
                PutService.Delete(x);
            }

            SendPurgeMessage("WeeklyIncomePlaySheet");
            foreach (WeeklyIncomePlaySheet x in WeeklyIncomePlaySheetService.GetCollection())
            {
                WeeklyIncomePlaySheetService.Delete(x);
            }

            OnRedirectRaised("WeeklyIncome", "Index", 0, this.JobId);
        }

        private void SendPurgeMessage(String entity)
        {
            OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
                        .Append(HtmlTags.B.Append(entity))
                        .Append(HtmlTags.Span.Append("&nbsp;being purged"))
                        .ToHtml().ToString(), this.JobId);
        }
        #endregion

        #region SecurityService_ProgressMessageRaised
        private void SecurityService_ProgressMessageRaised(object sender, ProgressMessageEventArgs e)
        {
            OnProgressMessageRaised(e.ProgressMessage, e.JobId);
        } 
        #endregion

        #region Dispose
        public void Dispose()
        {
        } 
        #endregion
    }
}
