using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Models
{
    public class StrategiesModel
    {
        private PullbackTradeTicketDto pullbackTradeTicket;

        public PullbackTradeTicketDto PullbackTradeTicket
        {
            get { return pullbackTradeTicket; }
            set { pullbackTradeTicket = value; }
        }

    }
}