using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Data.Models
{
    public class DayPerformanceModel
    {
        #region Properties
        public DateTime Date { get; set; }
        public String Day
        {
            get
            {
                return this.Date.DayOfWeek.ToString();
            }
        }

        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Trades { get; set; }
        public int Quantity { get; set; }
        public Decimal PnL { get; set; }

        public Double WinPtg
        {
            get
            {
                return (double)this.Wins / (double)this.Trades;
            }
        }

        public Decimal PPC
        {
            get
            {
                return this.PnL / this.Quantity;
            }
        }
        #endregion

        #region Ctors
        public DayPerformanceModel()
        {

        }

        public DayPerformanceModel(DateTime date, List<TradeTicket> tradeTickets)
        {
            this.Date = date;
            this.Wins = tradeTickets.Count(x => x.Won);
            this.Losses = tradeTickets.Count(x => !x.Won);
            this.Trades = tradeTickets.Count();
            this.Quantity = tradeTickets.Sum(x => x.Quantity);

            this.PnL = tradeTickets.Sum(x => x.PnL);
        }
        #endregion
    }
}