using Entities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeProAssistant.Data.Models;

namespace Services
{
	public class OptionChainService : OptionChainServiceBase
	{
        #region Custom Methods
        public static OptionChain GetOpcOptionChain(Security security)
        {
            int tries = 5;
            int currentTry = 1;

            while (currentTry < tries)
            {
                var client = new RestClient("https://www.optionsprofitcalculator.com/ajax/getOptions");
                var request = new RestRequest(Method.GET);
                request.AddParameter("stock", security.Symbol);
                request.AddParameter("reqId", 1);

                IRestResponse response = client.Execute(request);
                try
                {
                    String contentMassaged = response.Content.Replace(",\"_data_source\":\"c0\"", String.Empty);
                    OpcGetOptionChainResponse opcGetOptionChainResponse = JsonConvert.DeserializeObject<OpcGetOptionChainResponse>(contentMassaged);
                    OptionChain optionChain = new OptionChain(opcGetOptionChainResponse);

                    return optionChain;
                }
                catch(Exception ex)
                {
                    currentTry += 1;
                }
            }

            return null;
        }
        #endregion
    }
}

