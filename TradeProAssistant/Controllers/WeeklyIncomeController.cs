using AutoMapper;
using Data.Framework;
using Entities;
using Entities.Dtos;
using Hangfire;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using RestSharp;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TradeProAssistant.Framework;
using TradeProAssistant.Models;
using TradeProAssistant.Utilities;
using TradeProAssistant.Data.Framework;

namespace TradeProAssistant.Controllers
{
    public class WeeklyIncomeController : Controller
    {
        #region Ctor
        private readonly IMapper mapper;

        public WeeklyIncomeController()
        {
        }

        public WeeklyIncomeController(IMapper mapper)
        {
            this.mapper = mapper;
        } 
        #endregion

        #region Index
        // GET: WeeklyIncome
        public ActionResult Index()
        {
            Query query = new Query();
            query.SortPropertyName = Security.PropertyNames.Symbol;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.PairEligible,
                Parameter = "true",
                QueryOperator = QueryOperators.Equals,
                IsAndFilter = true
            });

            List<SecurityDto> list = mapper.Map<List<SecurityDto>>(SecurityService.GetCollection(query)).Where(x => x.AssetClassEnum == Enums.AssetClasses.Equity).ToList();
            //list.FilterForImportantDates();

            return View(list);
        }

        [HttpPost]
        public ActionResult Index(String Symbol)
        {
            WeeklyIncomeModel model = new WeeklyIncomeModel() { Symbol = Symbol };
            int tries = 5;
            int currentTry = 1;

            while (currentTry < tries)
            {
                var client = new RestClient("https://www.optionsprofitcalculator.com/ajax/getStockPrice");
                var request = new RestRequest(Method.GET);
                request.AddParameter("stock", Symbol);
                request.AddParameter("reqId", 0);

                IRestResponse response = client.Execute(request);
                OpcGetStockPriceResponse opcGetStockPriceResponse = JsonConvert.DeserializeObject<OpcGetStockPriceResponse>(response.Content);

                try
                {
                    model.Price = opcGetStockPriceResponse.Price.Last;

                    return View(model);
                }
                catch
                {
                    currentTry += 1;
                }
            }

            return View();
        }
        #endregion

        #region PlaySheet
        public ActionResult PlaySheet(int id)
        {
            List<String> includes = new List<string>();
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ComboCountsInclude);
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlansInclude);
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.PairsInclude);
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BullPutSpreadInclude);
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BullPutSpread.SecurityInclude);
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BullPutSpread.SellPutInclude);
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BullPutSpread.BuyPutInclude);
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BearCallSpreadInclude);
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BearCallSpread.SecurityInclude);
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BearCallSpread.SellCallInclude);
            includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BearCallSpread.BuyCallInclude);

            WeeklyIncomePlaySheetDto model = this.mapper.Map<WeeklyIncomePlaySheetDto>(WeeklyIncomePlaySheetService.Get(id, includes));

            return View(model);
        }
        #endregion

        #region PerformWeeklyIncomeActions
        [HttpPost]
        public ActionResult PerformWeeklyIncomeActions(int slots, Decimal minStrikeDiff, Decimal maxRisk, int strikePadding, List<int> actions)
        {
            maxRisk /= 200m;

            string jobId = Guid.NewGuid().ToString("N");
            ViewBag.JobId = jobId;
            BackgroundJob.Enqueue(() => PerformWeeklyIncomeActionsJob(jobId, slots, minStrikeDiff, strikePadding, maxRisk, actions));

            return View("ProgressLog");
        }

        public async System.Threading.Tasks.Task PerformWeeklyIncomeActionsJob(string jobId, int slots, Decimal minStrikeDiff, int strikePadding, Decimal maxRisk, List<int> actions)
        {
            using (WeeklyIncomeService service = new WeeklyIncomeService(jobId))
            {
                service.ProgressMessageRaised += Service_ProgressMessageRaised;
                service.RedirectRaised += Service_RedirectRaised;

                await service.PerformWeeklyIncomeActions(slots, minStrikeDiff, maxRisk, strikePadding, actions);
            }
        }
        #endregion

        #region BuildPlan
        //[HttpPost]
        //public ActionResult BuildPlan(int buildPlanSlots, int buildPlanMaxRisk)
        //{
        //    string jobId = Guid.NewGuid().ToString("N");
        //    ViewBag.JobId = jobId;
        //    BackgroundJob.Enqueue(() => BuildPlanJob(jobId, buildPlanSlots, buildPlanMaxRisk));

        //    return View("ProgressLog");
        //}

        //public async System.Threading.Tasks.Task BuildPlanJob(string jobId, int slots, Decimal maxRisk)
        //{
        //    using (WeeklyIncomeService service = new WeeklyIncomeService(jobId))
        //    {
        //        service.ProgressMessageRaised += Service_ProgressMessageRaised;
        //        service.RedirectRaised += Service_RedirectRaised;

        //        await service.BuildPlan(slots, maxRisk);
        //    }
        //}
        #endregion

        #region DownloadOptionChains
        [HttpPost]
        public ActionResult DownloadOptionChains()
        {
            string jobId = Guid.NewGuid().ToString("N");
            ViewBag.JobId = jobId;
            BackgroundJob.Enqueue(() => DownloadOptionChainsJob(jobId));

            return View("ProgressLog");
        }

        public async System.Threading.Tasks.Task DownloadOptionChainsJob(string jobId)
        {
            using (WeeklyIncomeService service = new WeeklyIncomeService(jobId))
            {
                service.ProgressMessageRaised += Service_ProgressMessageRaised;

                await service.DownloadOptionChains();
            }
        }
        #endregion

        #region ScrapeDates
        [HttpPost]
        public ActionResult ScrapeDates()
        {
            string jobId = Guid.NewGuid().ToString("N");
            ViewBag.JobId = jobId;
            BackgroundJob.Enqueue(() => ScrapeDatesJob(jobId));

            return View("ProgressLog");
        }

        public async System.Threading.Tasks.Task ScrapeDatesJob(string jobId)
        {
            using (SecurityService service = new SecurityService(jobId))
            {
                service.ProgressMessageRaised += Service_ProgressMessageRaised;

                await service.ScrapeDatesMaster();
            }
        }
        #endregion

        #region Event Handlers
        private void Service_ProgressMessageRaised(object sender, Data.Framework.ProgressMessageEventArgs e)
        {
            JobProgressHub.SendProgressMessage(e.ProgressMessage, e.JobId);
        }

        private void Service_RedirectRaised(object sender, Data.Framework.RedirectEventArgs e)
        {
            JobProgressHub.SendRedirect(e.Controller, e.Action, e.Id, e.JobId);
        }
        #endregion

        #region Securities_Read
        public ActionResult Securities_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = new DataSourceResult();
            Query query = request.ToQuery();
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.PairEligible,
                Parameter = "true",
                QueryOperator = QueryOperators.Equals,
                IsAndFilter = true
            });

            List<SecurityDto> securities = mapper.Map<List<SecurityDto>>(SecurityService.GetCollection(query));
            securities.FilterForImportantDates();

            result.Data = securities;
            result.Total = securities.Count;

            return new GuerillaLogisticsApiJsonResult(result);
        }
        #endregion

        #region PlaySheets_Read
        public ActionResult PlaySheets_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = new DataSourceResult();
            Query query = request.ToQuery();
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ComboCountsInclude);
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlansInclude);
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.PairsInclude);
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BullPutSpreadInclude);
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BullPutSpread.SecurityInclude);
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BullPutSpread.SellPutInclude);
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BullPutSpread.BuyPutInclude);
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BearCallSpreadInclude);
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BearCallSpread.SecurityInclude);
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BearCallSpread.SellCallInclude);
            query.Includes.Add(WeeklyIncomePlaySheet.PropertyNames.ActionPlans.Pairs.BearCallSpread.BuyCallInclude);

            result.Data = mapper.Map<List<SimpleWeeklyIncomePlaySheetDto>>(WeeklyIncomePlaySheetService.GetCollection(query));
            result.Total = WeeklyIncomePlaySheetService.GetCount(query);

            return new GuerillaLogisticsApiJsonResult(result);
        }
        #endregion

        #region Security Methods
        #region SetIgnore
        [HttpPost]
        public ActionResult SetIgnore(int identifier, bool ignore)
        {
            Security security = SecurityService.Get(identifier);
            security.Ignore = ignore;
            SecurityService.Save(security);

            return new EmptyResult();
        }
        #endregion

        #region SetIsBullish
        [HttpPost]
        public ActionResult SetIsBullish(int identifier, bool isBullish)
        {
            Security security = SecurityService.Get(identifier);
            security.IsBullish = isBullish;
            SecurityService.Save(security);

            return new EmptyResult();
        }
        #endregion

        #region SetIsBearish
        [HttpPost]
        public ActionResult SetIsBearish(int identifier, bool isBearish)
        {
            Security security = SecurityService.Get(identifier);
            security.IsBearish = isBearish;
            SecurityService.Save(security);

            return new EmptyResult();
        }
        #endregion

        #region SetIronCondorEligible
        [HttpPost]
        public ActionResult SetIronCondorEligible(int identifier, bool ironCondorEligible)
        {
            Security security = SecurityService.Get(identifier);
            security.IronCondorEligible = ironCondorEligible;
            SecurityService.Save(security);

            return new EmptyResult();
        }
        #endregion

        #region SetIronCondorEligible
        [HttpPost]
        public ActionResult SetSR(int identifier, Decimal support, Decimal resistance)
        {
            Security security = SecurityService.Get(identifier);
            security.Support = support;
            security.Resistance = resistance;
            SecurityService.Save(security);

            return new EmptyResult();
        }
        #endregion

        #region ClearBools
        [HttpPost]
        public ActionResult ClearBools()
        {
            Query query = new Query();
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.PairEligible,
                Parameter = "true",
                QueryOperator = QueryOperators.Equals,
                IsAndFilter = true
            });

            foreach (Security security in SecurityService.GetCollection(query))
            {
                security.Ignore = false;
                security.IsBullish = false;
                security.IsBearish = false;
                security.IronCondorEligible = false;
                security.Support = 0m;
                security.Resistance = 0m;
                SecurityService.Save(security);
            }

            return new EmptyResult();
        }
        #endregion

        #region GetNews
        [HttpPost]
        public async Task<ActionResult> GetNews(int benzingaId)
        {
            using (SecurityService service = new SecurityService())
            {
                List<Data.Models.BenzingaNewsStory> stories = await service.GetBenzingaNews(benzingaId);
                return PartialView("_NewsStories", stories);
            }
        }

        public async System.Threading.Tasks.Task GetNewsJob(string jobId, int benzingaId)
        {
            using (SecurityService service = new SecurityService(jobId))
            {
                //service.ProgressMessageRaised += Service_ProgressMessageRaised;

                await service.GetBenzingaNews(benzingaId);
            }
        }
        #endregion
        #endregion

        #region PlaySheet Methods
        #region SetUsed
        [HttpPost]
        public ActionResult SetUsed(int identifier, bool used)
        {
            WeeklyIncomePlaySheet playSheet = WeeklyIncomePlaySheetService.Get(identifier);
            playSheet.Used = used;
            WeeklyIncomePlaySheetService.Save(playSheet);

            return new EmptyResult();
        }
        #endregion 

        #region Purge
        [HttpPost]
        public ActionResult Purge()
        {
            string jobId = Guid.NewGuid().ToString("N");
            ViewBag.JobId = jobId;
            BackgroundJob.Enqueue(() => PurgeJob(jobId));

            return View("ProgressLog");
        }

        public async System.Threading.Tasks.Task PurgeJob(string jobId)
        {
            using (WeeklyIncomeService service = new WeeklyIncomeService(jobId))
            {
                service.ProgressMessageRaised += Service_ProgressMessageRaised;
                service.RedirectRaised += Service_RedirectRaised;
                service.Purge();
            }
        }
        #endregion
        #endregion
    }
}