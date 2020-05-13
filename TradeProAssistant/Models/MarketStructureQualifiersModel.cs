using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Models
{
    public class MarketStructureQualifiersModel
    {
        public MarketStructureQualifiers MarketStructureQualifier1 { get; set; }
        public MarketStructureQualifiers MarketStructureQualifier2 { get; set; }
        public MarketStructureQualifiers MarketStructureQualifier3 { get; set; }

        public MarketStructureQualifiersModel()
        {
            this.MarketStructureQualifier1 = MarketStructureQualifiers.None;
            this.MarketStructureQualifier2 = MarketStructureQualifiers.None;
            this.MarketStructureQualifier3 = MarketStructureQualifiers.None;
        }
    }
}