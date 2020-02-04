using Data.Framework;
using Entities;
using Enums;
using HtmlBuilders;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TradeProAssistant.Data.Models;
using System.Linq;
using TradeProAssistant.Data.Framework;

namespace Services
{
    public class SecurityService : SecurityServiceBase, IDisposable
	{
        const String YahooUrl = "https://finance.yahoo.com/quote/{0}";
        const String BenzingaUrl = "https://www.benzinga.com/stock/{0}/";
        const String BenzingaNewsUrl = "https://www.benzinga.com/services/webapps/content?keyed=true&parameters[is_bz_post]=false&parameters[is_bzpro_post]=true&parameters[tids]={0}&parameters[type]=story,scoutfin_realtimebriefs";

        #region Regex
        //<li id="news-headlines".*"tids":"(?<tids>\d+)"
        private static Regex _regexExDividendDate = new Regex(@"<span data-reactid=""114"">(?<ExDividendDate>.{3} \d{2}, \d{4})", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static Regex _regexNextEarningsDate = new Regex(@"<span data-reactid=""105"">(?<NextEarningsDate>.{3} \d{2}, \d{4})", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static Regex _regexBenzingaId = new Regex(@"<li id=""news-headlines"".*?""tids"":""(?<tids>\d+)""", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);
        #endregion

        #region Properties
        public String JobId { get; set; }
        #endregion

        #region Constructors
        public SecurityService()
        {

        }

        public SecurityService(String jobId)
        {
            this.JobId = jobId;
        }
        #endregion

        #region Custom Methods
        #region GetCurrentPrice
        public static bool GetCurrentPrice(Security security)
        {
            int tries = 5;
            int currentTry = 1;

            while (currentTry < tries)
            {
                var client = new RestClient("https://www.optionsprofitcalculator.com/ajax/getStockPrice");
                var request = new RestRequest(Method.GET);
                request.AddParameter("stock", security.Symbol);
                request.AddParameter("reqId", 0);

                IRestResponse response = client.Execute(request);
                OpcGetStockPriceResponse opcGetStockPriceResponse = JsonConvert.DeserializeObject<OpcGetStockPriceResponse>(response.Content);

                try
                {
                    security.CurrentPrice = opcGetStockPriceResponse.Price.Last;

                    return true;
                }
                catch
                {
                    currentTry += 1;
                }
            }

            return false;
        }
        #endregion

        #region ScrapeDatesMaster
        public async Task ScrapeDatesMaster()
        {
            Thread.Sleep(1000);

            Query query = new Query();
            query.SortPropertyName = Security.PropertyNames.Symbol;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.PairEligible,
                Parameter = "true",
                QueryOperator = QueryOperators.Equals
            });

            List<Security> securities = SecurityService.GetCollection(query);

            for (int i = 0; i < securities.Count; i++)
            {
                Security security = securities[i];
                await ScrapeDates(security);
            }
        }
        #endregion

        #region ScrapeDates
        public async Task ScrapeDates(Security security)
        {
            OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
                        .Append(HtmlTags.Span.Append("Scraping dates for&nbsp;"))
                        .Append(HtmlTags.B.Append(security.Symbol))
                        .ToHtml().ToString(), this.JobId);

            using (WebClient webClient = new WebClient())
            {
                String html = await webClient.DownloadStringTaskAsync(String.Format(YahooUrl, security.Symbol));
                var matches = _regexExDividendDate.Matches(html);

                security.ExDividendDate = null;
                DateTime exDividendDate = DateTime.MinValue;
                if (matches.Count > 0 && DateTime.TryParse(matches[0].Groups["ExDividendDate"].Value, out exDividendDate))
                {
                    security.ExDividendDate = exDividendDate.AdjustDateForWeekend();
                }

                matches = _regexNextEarningsDate.Matches(html);
                security.NextEarningsDate = null;
                DateTime nextEarningsDate = DateTime.MinValue;
                if (matches.Count > 0 && DateTime.TryParse(matches[0].Groups["NextEarningsDate"].Value, out nextEarningsDate))
                {
                    security.NextEarningsDate = nextEarningsDate.AdjustDateForWeekend();
                }

                Save(security);
            }
        }
        #endregion

        #region ScrapeBenzingaIdsMaster
        public async Task ScrapeBenzingaIdsMaster()
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
            //query.QuerySingleFilters.Add(new QuerySingleFilter()
            //{
            //    PropertyName = Security.PropertyNames.AssetClassEnum,
            //    Parameter = ((int)AssetClasses.Equity).ToString(),
            //    QueryOperator = QueryOperators.Equals,
            //    IsAndFilter = true
            //});

            List<Security> securities = SecurityService.GetCollection(query).Where(x => x.AssetClassEnum == AssetClasses.Equity).ToList();

            for (int i = 0; i < securities.Count; i++)
            {
                Security security = securities[i];
                await ScrapeBenzingaIds(security);
            }
        }
        #endregion

        #region ScrapeBenzingaIds
        public async Task ScrapeBenzingaIds(Security security)
        {
            OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
                        .Append(HtmlTags.Span.Append("Scraping Benzinga ID for&nbsp;"))
                        .Append(HtmlTags.B.Append(security.Symbol))
                        .ToHtml().ToString(), this.JobId);

            using (WebClient webClient = new WebClient())
            {
                String html = await webClient.DownloadStringTaskAsync(String.Format(BenzingaUrl, security.Symbol));
                var matches = _regexBenzingaId.Matches(html);

                security.BenzingaId = 0;
                int benzingaId = 0;
                if (matches.Count > 0 && Int32.TryParse(matches[0].Groups["tids"].Value, out benzingaId))
                {
                    security.BenzingaId = benzingaId;
                }

                Save(security);
            }
        }
        #endregion

        #region GetBenzingaNews
        public async Task<List<BenzingaNewsStory>> GetBenzingaNews(int benzingaId)
        {
            using (WebClient webClient = new WebClient())
            {
                String html = await webClient.DownloadStringTaskAsync(String.Format(BenzingaNewsUrl, benzingaId));
                BenzingaNewsResponse response = JsonConvert.DeserializeObject<BenzingaNewsResponse>(html);
                return response.GetStoriesList();
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {

        } 
        #endregion
        #endregion
    }
}

