using Contexts;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using TradeProAssistant.Data.Models;

namespace Services
{
    public class TradeTicketService : TradeTicketServiceBase
    {
        #region Custom Methods
        public static List<DayPerformanceModel> GetDayPerformance()
        {
            List<DayPerformanceModel> list = new List<DayPerformanceModel>();

            List<TradeTicket> tradeTickets = GetCollection();

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
                list.Add(new DayPerformanceModel(g.Date, g.TradeTickets));
            }

            return list;
        }

        public static MonthPerformanceModel GetMonthPerformance(DateTime start)
        {
            using (TradeProAssistantContext context = new TradeProAssistantContext())
            {
                DateTime end = start.AddDays(28);
                List<TradeTicket> tradeTickets = context.TradeTickets.Where(x => x.Timestamp >= start && x.Timestamp < end).ToList();
                return new MonthPerformanceModel(start, tradeTickets);
            }
        }
        #endregion
    }
}
