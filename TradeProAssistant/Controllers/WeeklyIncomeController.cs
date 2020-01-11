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

        [HttpPost]
        public ActionResult GenerateWeeklyIncome()
        {
            string jobId = Guid.NewGuid().ToString("N");
            ViewBag.JobId = jobId;
            BackgroundJob.Enqueue(() => GenerateWeeklyIncomeJob(jobId));

            return View();
        }

        public async System.Threading.Tasks.Task GenerateWeeklyIncomeJob(string jobId)
        {
            using (WeeklyIncomeService service = new WeeklyIncomeService(jobId))
            {
                service.ProgressMessageRaised += Service_ProgressMessageRaised;

                await service.GenerateWeeklyIncome();
            }
        }

        private void Service_ProgressMessageRaised(object sender, Data.Framework.ProgressMessageEventArgs e)
        {
            JobProgressHub.SendProgressMessage(e.ProgressMessage, e.JobId);
        }
    }
}