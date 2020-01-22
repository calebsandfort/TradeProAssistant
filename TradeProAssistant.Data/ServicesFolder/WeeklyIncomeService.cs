using Data.Framework;
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
        public async Task PerformWeeklyIncomeActions(int slots, Decimal minStrikeDiff, Decimal maxRisk, List<int> actions)
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
                    case WeeklyIncomeActions.GeneratePlaySheet:
                        await BuildPlan(slots, minStrikeDiff, maxRisk);
                        break;
                }
            }
        }
        #endregion

        #region BuildPlan
        public async Task BuildPlan(int slots, Decimal minStrikeDiff, Decimal maxRisk)
        {
            Thread.Sleep(1000);

            DateTime monday = DateTime.Now.GetPreviousDayOfWeekOccurrence(DayOfWeek.Monday);
            DateTime friday = DateTime.Now.GetNextDayOfWeekOccurrence(DayOfWeek.Friday);

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

            securities.RemoveAll(x => (x.NextEarningsDate.HasValue && x.NextEarningsDate >= monday && x.NextEarningsDate <= friday));
            securities.RemoveAll(x => (x.ExDividendDate.HasValue && x.ExDividendDate >= monday && x.ExDividendDate <= friday));

            //securities = securities.Where(x => x.SectorEnum == Enums.Sectors.Index).ToList();

            OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item").Class("active")
                        .Append(HtmlTags.B.Append(securities.Count.ToString()))
                        .Append(HtmlTags.Span.Append("&nbsp;optionable securities retrieved"))
                        .ToHtml().ToString(), this.JobId);

            foreach(Security security in securities)
            {
                security.MinStrikeDiff = minStrikeDiff;
                security.MaxRisk = maxRisk;

                OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
                .Append(HtmlTags.B.Append($"{security.Symbol}"))
                .Append(HtmlTags.Span.Append("&nbsp;expected move:&nbsp;"))
                .Append(HtmlTags.B.Append($"{security.ExpectedMove:C} ({(security.ExpectedMove / security.CurrentPrice):P2})"))
                .Append(HtmlTags.Span.Append("&nbsp;credit spreads:&nbsp;"))
                .Append(HtmlTags.B.Append($"{security.BearCallSpread}, {security.BullPutSpread}"))
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
                                ijPairCondor.BullPutSpread = iSecurity.BullPutSpread;
                                ijPairCondor.BearCallSpread = jSecurity.BearCallSpread;
                                pairCondorCandidates.Add(ijPairCondor); 
                            }

                            if (!jSecurity.IsBearish && !iSecurity.IsBullish)
                            {
                                PairCondor jiPairCondor = new PairCondor();
                                jiPairCondor.SectorEnum = sectorGroup.Sector;
                                jiPairCondor.BullPutSpread = jSecurity.BullPutSpread;
                                jiPairCondor.BearCallSpread = iSecurity.BearCallSpread;
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

            int playSheetId = WeeklyIncomePlaySheetService.Save(playSheet);
            pairCondorCandidates = pairCondorCandidates.OrderByDescending(x => x.Credit).Select(x => new PairCondor(x)).ToList();
            WeeklyIncomeActionPlan bruteForceActionPlan = BruteForceActionPlanGenerator(pairCondorCandidates, slots);
            bruteForceActionPlan.PlaySheetIdentifier = playSheetId;
            WeeklyIncomeActionPlanService.Save(bruteForceActionPlan);

            pairCondorCandidates = pairCondorCandidates.OrderByDescending(x => x.Credit).Select(x => new PairCondor(x)).ToList();
            WeeklyIncomeActionPlan randomSearchActionPlan = RandomSearchActionPlanGenerator(pairCondorCandidates, slots);
            randomSearchActionPlan.PlaySheetIdentifier = playSheetId;
            WeeklyIncomeActionPlanService.Save(randomSearchActionPlan);

            pairCondorCandidates = pairCondorCandidates.OrderByDescending(x => x.Credit).Select(x => new PairCondor(x)).ToList();
            WeeklyIncomeActionPlan geneticOptimizationActionPlan = GeneticOptimizationActionPlanGenerator(pairCondorCandidates, slots);
            geneticOptimizationActionPlan.PlaySheetIdentifier = playSheetId;
            WeeklyIncomeActionPlanService.Save(geneticOptimizationActionPlan);

            Thread.Sleep(1000);
            OnRedirectRaised("WeeklyIncome", "PlaySheet", playSheetId, this.JobId);
        }
        #endregion

        #region ActionPlanGenerators
        #region BruteForceActionPlanGenerator
        private WeeklyIncomeActionPlan BruteForceActionPlanGenerator(List<PairCondor> pairCondorCandidates, int slots)
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

        #region RandomSearchActionPlanGenerator
        private WeeklyIncomeActionPlan RandomSearchActionPlanGenerator(List<PairCondor> pairCondorCandidates,
            int slots, int iterations = 500)
        {
            WeeklyIncomeActionPlan best = BuildRandomSolution(pairCondorCandidates, WeeklyActionPlanGenerationMethods.RandomSearch, slots);

            for(int i = 0; i < iterations; i++)
            {
                WeeklyIncomeActionPlan temp = BuildRandomSolution(pairCondorCandidates, WeeklyActionPlanGenerationMethods.RandomSearch, slots);
                if(temp.Credit > best.Credit)
                {
                    best = temp;
                }
            }

            return best;
        }
        #endregion

        #region GeneticOptimizationActionPlanGenerator
        private WeeklyIncomeActionPlan GeneticOptimizationActionPlanGenerator(List<PairCondor> pairCondorCandidates,
            int slots, int popSize = 50, Double mutProb = .2, Double elite = .2, int maxIter = 100)
        {
            List<WeeklyIncomeActionPlan> pop = new List<WeeklyIncomeActionPlan>();
            for(int i = 0; i < popSize; i++)
            {
                pop.Add(BuildRandomSolution(pairCondorCandidates, WeeklyActionPlanGenerationMethods.GeneticOptimization, slots));
            }

            int topElite = (int)(elite * popSize);

            for(int i = 0; i < maxIter; i++)
            {
                pop = pop.OrderByDescending(x => x.Credit).Take(topElite).ToList();

                while(pop.Count < popSize)
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

            if(pairIdx < 0)
            {
                pairIdx = pairCondorCandidates.Count - 1;
            }
            else if( pairIdx > (pairCondorCandidates.Count - 1))
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

            for(int i = splitIdx; i < r2.Pairs.Count; i++)
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
