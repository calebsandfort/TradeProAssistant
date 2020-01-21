using AutoMapper;
using Entities;
using Entities.Dtos;
using Hangfire;
using Newtonsoft.Json;
using RestSharp;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeProAssistant.Models;
using TradeProAssistant.Utilities;

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
            return View(new WeeklyIncomeModel() { Symbol = "FB" });
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

        #region WeeklyIncomeActions
        [HttpPost]
        public ActionResult PerformWeeklyIncomeActions(int slots, Decimal maxRisk, List<int> actions)
        {
            maxRisk /= 200m;

            string jobId = Guid.NewGuid().ToString("N");
            ViewBag.JobId = jobId;
            BackgroundJob.Enqueue(() => PerformWeeklyIncomeActionsJob(jobId, slots, maxRisk, actions));

            return View("ProgressLog");
        }

        public async System.Threading.Tasks.Task PerformWeeklyIncomeActionsJob(string jobId, int slots, Decimal maxRisk, List<int> actions)
        {
            using (WeeklyIncomeService service = new WeeklyIncomeService(jobId))
            {
                service.ProgressMessageRaised += Service_ProgressMessageRaised;
                service.RedirectRaised += Service_RedirectRaised;

                await service.PerformWeeklyIncomeActions(slots, maxRisk, actions);
            }
        }
        #endregion

        #region BuildPlan
        [HttpPost]
        public ActionResult BuildPlan(int buildPlanSlots, int buildPlanMaxRisk)
        {
            string jobId = Guid.NewGuid().ToString("N");
            ViewBag.JobId = jobId;
            BackgroundJob.Enqueue(() => BuildPlanJob(jobId, buildPlanSlots, buildPlanMaxRisk));

            return View("ProgressLog");
        }

        public async System.Threading.Tasks.Task BuildPlanJob(string jobId, int slots, Decimal maxRisk)
        {
            using (WeeklyIncomeService service = new WeeklyIncomeService(jobId))
            {
                service.ProgressMessageRaised += Service_ProgressMessageRaised;
                service.RedirectRaised += Service_RedirectRaised;

                await service.BuildPlan(slots, maxRisk);
            }
        }
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
    }
}