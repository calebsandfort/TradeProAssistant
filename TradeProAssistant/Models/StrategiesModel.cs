using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Models
{
    public class StrategiesModel
    {
        private TradeTicketModel tradeTicket;

        public TradeTicketModel TradeTicket
        {
            get { return tradeTicket; }
            set { tradeTicket = value; }
        }

    }
}