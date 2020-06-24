using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Data.Models
{
    public sealed class DayPerformanceModel : PerformanceModel
    {
        #region Properties
        public String Day
        {
            get
            {
                return this.Date.DayOfWeek.ToString();
            }
        }
        #endregion

        #region Ctors
        public DayPerformanceModel(DateTime date, List<TradeTicket> tradeTickets) : base(date, tradeTickets)
        {
            
        }
        #endregion
    }
}