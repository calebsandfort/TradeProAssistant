using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TradeProAssistant.Data.Framework;
using System.Globalization;

namespace Services
{
    public class EconomicDayService : EconomicDayServiceBase, IDisposable
    {
        const String ForexFactoryUrl = "https://www.forexfactory.com/calendar.php?day={0:MMMd.yyyy}";

        #region Custom Methods
        #region ScrapeForexFactoryMonth
        public async Task ScrapeForexFactory(DateTime start, DateTime end)
        {
            List<String> eventsList = new List<string>();
            while (start <= end)
            {
                OnProgressMessageRaised(String.Format($"Scraping {start}"), "Trace");
                if (start.DayOfWeek == DayOfWeek.Saturday) start = start.AddDays(2);
                if (start.DayOfWeek == DayOfWeek.Sunday) start = start.AddDays(1);
                await ScrapeForexFactoryDay(start, eventsList);
                start = start.AddDays(1);
            }

            //<EnumMember Name="OneMinute" Value="1" DisplayValue="1 Minute"/>
            int idx = 0;
            TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;

            foreach (String e in eventsList.OrderBy(x => x))
            {
                idx += 1;
                OnProgressMessageRaised(String.Format($"<EnumMember Name=\"{txtInfo.ToTitleCase(e).Replace("/", "Over").Replace(" ", String.Empty)}\" Value=\"{idx}\" DisplayValue=\"{e}\"/>"), String.Empty);
            }
        }
        #endregion

        #region ScrapeForexFactoryMonth
        public async Task ScrapeForexFactoryDay(DateTime day, List<String> eventsList)
        {
            using (WebClient webClient = new WebClient())
            {
                String html = await webClient.DownloadStringTaskAsync(String.Format(ForexFactoryUrl, day));
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                List<HtmlNode> eventRows = doc.DocumentNode.QuerySelectorAll("table.calendar__table tr[data-eventid][data-touchable]").ToList();
                //OnProgressMessageRaised(String.Format($"Found {eventRows.Count} events"), String.Empty);

                DateTime rollingTimestamp = day.Date;
                foreach(HtmlNode eventRow in eventRows)
                {
                    HtmlNode currencyCell = eventRow.QuerySelector(".currency");
                    String currencyString = currencyCell.InnerText.RemoveNewLines();
                    if (currencyString != "USD") continue;

                    HtmlNode impactCell = eventRow.QuerySelector(".impact");
                    if (!impactCell.Attributes["class"].Value.Contains("calendar__impact--high")) continue;

                    HtmlNode timeCell = eventRow.QuerySelector(".time");
                    String timeString = timeCell.InnerText.RemoveNewLines();
                    if (timeString == "All Day") continue;

                    if (!String.IsNullOrEmpty(timeString))
                    {
                        rollingTimestamp = DateTime.Parse($"{day.Date.ToShortDateString()} {timeString}").AddHours(-3);
                        //OnProgressMessageRaised(String.Format($"rollingTimestamp = {rollingTimestamp}"), String.Empty);
                    }

                    HtmlNode eventTitleCell = eventRow.QuerySelector(".calendar__event-title");
                    String eventTitleString = eventTitleCell.InnerText.RemoveNewLines();
                    //OnProgressMessageRaised(String.Format($"Event title = {eventTitleString}"), String.Empty);

                    if (!eventsList.Contains(eventTitleString)) eventsList.Add(eventTitleString);
                }
            }
        }
        #endregion
        #endregion

        #region Dispose
        public void Dispose()
        {
            
        } 
        #endregion
    }
}

