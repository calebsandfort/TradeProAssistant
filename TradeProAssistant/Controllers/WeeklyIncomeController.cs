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

        #region BuildPlan
        [HttpPost]
        public ActionResult BuildPlan()
        {
            string jobId = Guid.NewGuid().ToString("N");
            ViewBag.JobId = jobId;
            BackgroundJob.Enqueue(() => BuildPlanJob(jobId));

            return View("ProgressLog");
        }

        public async System.Threading.Tasks.Task BuildPlanJob(string jobId)
        {
            using (WeeklyIncomeService service = new WeeklyIncomeService(jobId))
            {
                service.ProgressMessageRaised += Service_ProgressMessageRaised;

                await service.BuildPlan();
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

        private void Service_ProgressMessageRaised(object sender, Data.Framework.ProgressMessageEventArgs e)
        {
            JobProgressHub.SendProgressMessage(e.ProgressMessage, e.JobId);
        }
    }
}