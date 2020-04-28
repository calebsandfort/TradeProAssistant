using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace TradeProAssistant.Tests
{
    [TestClass]
    public class EconomicDayServiceTests
    {
        [TestMethod]
        [Timeout(TestTimeout.Infinite)]
        public async Task ScrapeForexFactoryTest()
        {
            using (EconomicDayService service = new EconomicDayService())
            {
                service.ProgressMessageRaised += Service_ProgressMessageRaised;

                DateTime start = new DateTime(2019, 1, 1);
                //DateTime end = new DateTime(2019, 1, 15);
                DateTime end = new DateTime(2020, 12, 31);
                await service.ScrapeForexFactory(start, end);
            }
        }

        [TestMethod]
        public async Task ScrapeForexFactoryDayTest()
        {
            using (EconomicDayService service = new EconomicDayService())
            {
                service.ProgressMessageRaised += Service_ProgressMessageRaised;

                DateTime feb2020 = new DateTime(2019, 11, 22);
                await service.ScrapeForexFactoryDay(feb2020, new List<string>());
            }
        }

        private void Service_ProgressMessageRaised(object sender, Data.Framework.ProgressMessageEventArgs e)
        {
            switch (e.JobId)
            {
                case "Trace":
                    Trace.WriteLine(e.ProgressMessage);
                    break;
                default:
                    Console.WriteLine(e.ProgressMessage);
                    break;
            }
        }
    }
}
