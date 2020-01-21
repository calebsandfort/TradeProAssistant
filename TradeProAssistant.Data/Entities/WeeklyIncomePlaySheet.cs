using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class WeeklyIncomePlaySheet
	{
		[NotMapped]
		public static WeeklyIncomePlaySheetPropertyNames PropertyNames = new WeeklyIncomePlaySheetPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public DateTime TimeStamp { get; set; }

		public DateTime Expiry { get; set; }

		public virtual List<WeeklyIncomeActionPlan> ActionPlans { get; set; }
		public virtual List<WeeklyIncomeComboCount> ComboCounts { get; set; }

		#region Constructor
		public  WeeklyIncomePlaySheet()
		{
			ActionPlans = new List<WeeklyIncomeActionPlan>();
	ComboCounts = new List<WeeklyIncomeComboCount>();
		}

		public  WeeklyIncomePlaySheet(WeeklyIncomePlaySheet source)
		{
			this.TimeStamp = source.TimeStamp;
			this.Expiry = source.Expiry;
					this.ActionPlans = source.ActionPlans.Select(x => new WeeklyIncomeActionPlan(x)).ToList();
			this.ComboCounts = source.ComboCounts.Select(x => new WeeklyIncomeComboCount(x)).ToList();
		}
		#endregion
	}
}
