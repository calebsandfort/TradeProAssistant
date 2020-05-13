using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
	public partial class RiskParameters
	{
		[NotMapped]
		public static RiskParametersPropertyNames PropertyNames = new RiskParametersPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		[MaxLength(50)]
		public String Name { get; set; }

		public bool Active { get; set; }

		public Decimal TpaDailyTarget { get; set; }

		public Decimal TpaWeeklyTarget { get; set; }

		public Decimal TpaMonthlyTarget { get; set; }

		public Decimal MyDailyTarget { get; set; }

		public Decimal MyWeeklyTarget { get; set; }

		public Decimal MyMonthlyTarget { get; set; }

		public Decimal DailyStop { get; set; }

		public Decimal WeeklyStop { get; set; }

		public Decimal MonthlyStop { get; set; }

		#region Constructor
		public  RiskParameters()
		{
				}

		public  RiskParameters(RiskParameters source)
		{
			this.Name = source.Name;
			this.Active = source.Active;
			this.TpaDailyTarget = source.TpaDailyTarget;
			this.TpaWeeklyTarget = source.TpaWeeklyTarget;
			this.TpaMonthlyTarget = source.TpaMonthlyTarget;
			this.MyDailyTarget = source.MyDailyTarget;
			this.MyWeeklyTarget = source.MyWeeklyTarget;
			this.MyMonthlyTarget = source.MyMonthlyTarget;
			this.DailyStop = source.DailyStop;
			this.WeeklyStop = source.WeeklyStop;
			this.MonthlyStop = source.MonthlyStop;
				}
		#endregion
	}
}
