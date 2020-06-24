using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeProAssistant.Data.Models
{
    public sealed class WeekPerformanceModel : PerformanceModel
    {
        #region Properties
        public List<DayPerformanceModel> Days { get; set; }
        #endregion

        #region Ctors
        public WeekPerformanceModel(DateTime date, List<TradeTicket> tradeTickets) : base(date, tradeTickets)
        {
            if (tradeTickets.Count == 0) return;

            this.Days = new List<DayPerformanceModel>();

            var groupings = from tt in tradeTickets
                            group tt by tt.Timestamp.Date into g
                            orderby g.Key descending
                            select new
                            {
                                Date = g.Key,
                                TradeTickets = g.ToList()
                            };

            foreach (var g in groupings)
            {
                this.Days.Add(new DayPerformanceModel(g.Date, g.TradeTickets));
            }
        }
        #endregion
    }
}
