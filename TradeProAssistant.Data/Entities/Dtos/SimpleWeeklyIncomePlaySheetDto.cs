using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Dtos
{
	public class SimpleWeeklyIncomePlaySheetDto : WeeklyIncomePlaySheetDto
    {
        #region TimeStampDisplay
        public String TimeStampDisplay
        {
            get
            {
                return this.TimeStamp.ToString("g");
            }
        }
        #endregion

        #region Credit
        public Decimal Credit
        {
            get
            {
                return this.ActionPlans.Count == 0 ? 0m : this.ActionPlans.Max(x => x.Credit);
            }
        }
        #endregion

        #region Slots
        public int Slots
        {
            get
            {
                return this.ActionPlans.Count == 0 ? 0 : this.ActionPlans.First().Pairs.Count;
            }
        }
        #endregion

        #region RequiredCapital
        public Decimal RequiredCapital
        {
            get
            {
                return this.ActionPlans.Count == 0 ? 0m : this.ActionPlans.OrderByDescending(x => x.Credit).First().RequiredCapital;
            }
        }
        #endregion

        #region RiskPerSlot
        public Decimal RiskPerSlot
        {
            get
            {
                return this.Slots == 0 ? 0m : this.RequiredCapital / this.Slots;
            }
        }
        #endregion
    }
}
