using Data.Framework;
using Entities;
using HtmlBuilders;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TradeProAssistant.Data.Framework;
using System.Linq;

namespace Services
{
    public class WeeklyIncomeService : ServiceBase, IDisposable
    {
        #region Properties
        public String JobId { get; set; }
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

        #region BuildPlan
        public async Task BuildPlan()
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

                            PairCondor ijPairCondor = new PairCondor();
                            ijPairCondor.SectorEnum = sectorGroup.Sector;
                            ijPairCondor.BullPutSpread = iSecurity.BullPutSpread;
                            ijPairCondor.BearCallSpread = jSecurity.BearCallSpread;
                            pairCondorCandidates.Add(ijPairCondor);

                            PairCondor jiPairCondor = new PairCondor();
                            jiPairCondor.SectorEnum = sectorGroup.Sector;
                            jiPairCondor.BullPutSpread = jSecurity.BullPutSpread;
                            jiPairCondor.BearCallSpread = iSecurity.BearCallSpread;
                            pairCondorCandidates.Add(jiPairCondor);

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

            playSheet.ActionPlans.Add(BruteForceActionPlanGenerator(new List<PairCondor>(pairCondorCandidates)));
            playSheet.ActionPlans.Add(RandomSearchActionPlanGenerator(new List<PairCondor>(pairCondorCandidates)));

            int playSheetId = WeeklyIncomePlaySheetService.Save(playSheet);

            Thread.Sleep(1000);
            OnRedirectRaised("WeeklyIncome", "PlaySheet", playSheetId, this.JobId);
        }
        #endregion

        #region ActionPlanGenerators
        #region BruteForceActionPlanGenerator
        private WeeklyIncomeActionPlan BruteForceActionPlanGenerator(List<PairCondor> pairCondorCandidates, int slots = 5)
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
            int slots = 5, int iterations = 500)
        {
            WeeklyIncomeActionPlan best = RandomSearchBuildSolution(pairCondorCandidates, slots);

            for(int i = 0; i < iterations; i++)
            {
                WeeklyIncomeActionPlan temp = RandomSearchBuildSolution(pairCondorCandidates, slots);
                if(temp.Credit > best.Credit)
                {
                    best = temp;
                }
            }

            return best;
        }

        private WeeklyIncomeActionPlan RandomSearchBuildSolution(List<PairCondor> pairCondorCandidates, int slots = 5)
        {
            WeeklyIncomeActionPlan actionPlan = new WeeklyIncomeActionPlan();
            actionPlan.GenerationMethod = Enums.WeeklyActionPlanGenerationMethods.RandomSearch;

            Random random = new Random(DateTime.Now.Millisecond);

            while(actionPlan.Pairs.Count < slots)
            {
                int idx = random.Next(pairCondorCandidates.Count);
                actionPlan.AddPairCondor(pairCondorCandidates[idx]);
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

        #region Dispose
        public void Dispose()
        {
        } 
        #endregion
    }
}
