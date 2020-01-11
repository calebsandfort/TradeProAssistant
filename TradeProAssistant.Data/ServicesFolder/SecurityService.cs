using Entities;
using Newtonsoft.Json;
using RestSharp;
using TradeProAssistant.Data.Models;

namespace Services
{
    public class SecurityService : SecurityServiceBase
	{
		#region Custom Methods
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
	}
}

