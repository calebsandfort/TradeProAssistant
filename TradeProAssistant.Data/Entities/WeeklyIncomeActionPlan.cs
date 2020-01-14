using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class WeeklyIncomeActionPlan
	{
		[NotMapped]
		public static WeeklyIncomeActionPlanPropertyNames PropertyNames = new WeeklyIncomeActionPlanPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public Decimal Credit { get; set; }

		public Decimal Risk { get; set; }

		public DateTime TimeStamp { get; set; }

		public DateTime Expiry { get; set; }

		public virtual List<PairCondor> Pairs { get; set; }

		#region Constructor
		public  WeeklyIncomeActionPlan()
		{
			Pairs = new List<PairCondor>();
		}
		#endregion
	}
}
