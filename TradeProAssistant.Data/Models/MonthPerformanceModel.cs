using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeProAssistant.Data.Models
{
    public sealed class MonthPerformanceModel : PerformanceModel
    {
        #region Properties
        public List<WeekPerformanceModel> Weeks { get; set; }
        public DayPerformanceModel CurrentDayPerformanceModel { get; set; }
        public WeekPerformanceModel CurrentWeekPerformanceModel { get; set; }
        #endregion

        #region Ctors
        public MonthPerformanceModel(DateTime date, List<TradeTicket> tradeTickets) : base(date, tradeTickets)
        {
            this.Weeks = new List<WeekPerformanceModel>();

            if (tradeTickets.Count == 0) return;

            DateTime currentDate = date;
            DateTime lastDate = tradeTickets.Max(x => x.Timestamp);

            while(currentDate < lastDate)
            {
                this.Weeks.Add(new WeekPerformanceModel(currentDate, tradeTickets.Where(x => x.Timestamp >= currentDate && x.Timestamp < currentDate.AddDays(7)).ToList()));
                currentDate = currentDate.AddDays(7);
            }

            DateTime now = DateTime.Now.Date;

            this.CurrentDayPerformanceModel = new DayPerformanceModel(DateTime.Now.Date, tradeTickets.Where(x => x.Timestamp.Date == DateTime.Now.Date).ToList());

            if(this.Weeks.Any(x => now >= x.Date && now < x.Date.AddDays(7)))
            {
                this.CurrentWeekPerformanceModel = this.Weeks.First(x => now >= x.Date && now < x.Date.AddDays(7));
            }
        }
        #endregion
    }
}
