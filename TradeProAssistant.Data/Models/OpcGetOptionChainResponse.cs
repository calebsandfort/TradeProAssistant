using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Data.Models
{
    public class OpcGetOptionChainResponse
    {
        [JsonProperty("options")]
        public Dictionary<string, OpcDate> Dates { get; set; }
    }

    public class OpcDate
    {
        [JsonProperty("c")]
        public Dictionary<string, OpcOptionPriceSpread> Calls { get; set; }

        [JsonProperty("p")]
        public Dictionary<string, OpcOptionPriceSpread> Puts { get; set; }
    }

    public class OpcOptionPriceSpread
    {
        [JsonProperty("b")]
        public Decimal Bid { get; set; }

        [JsonProperty("a")]
        public Decimal Ask { get; set; }
    }
}