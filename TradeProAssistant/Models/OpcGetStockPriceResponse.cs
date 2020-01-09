using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Models
{
    public class OpcGetStockPriceResponse
    {
        [JsonProperty("price")]
        public OpcGetStockPrice Price { get; set; }
    }

    public class OpcGetStockPrice
    {
        [JsonProperty("last")]
        public Decimal Last { get; set; }

        [JsonProperty("bid")]
        public Decimal Bid { get; set; }

        [JsonProperty("ask")]
        public Decimal Ask { get; set; }
    }
}