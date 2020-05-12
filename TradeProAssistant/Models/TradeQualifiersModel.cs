using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Models
{
    public class TradeQualifiersModel
    {
        public TradeQualifierTypes TradeQualifierType { get; set; }
        public List<TradeQualifiers> TradeQualifiersList { get; set; }

        public TradeQualifiersModel()
        {
            this.TradeQualifiersList = new List<TradeQualifiers>();
        }
    }
}