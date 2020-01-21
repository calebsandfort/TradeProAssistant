using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

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

		public Int32 Dummy { get; set; }

		public WeeklyActionPlanGenerationMethods GenerationMethod { get; set; }

		public int? PlaySheetIdentifier { get; set; }
		public virtual WeeklyIncomePlaySheet PlaySheet { get; set; }

		public virtual List<PairCondor> Pairs { get; set; }

		#region Constructor
		public  WeeklyIncomeActionPlan()
		{
			Pairs = new List<PairCondor>();
		}

		public  WeeklyIncomeActionPlan(WeeklyIncomeActionPlan source)
		{
			this.Dummy = source.Dummy;
			this.GenerationMethod = source.GenerationMethod;
			if(source.PlaySheet != null) this.PlaySheet = new WeeklyIncomePlaySheet(source.PlaySheet);
			this.Pairs = source.Pairs.Select(x => new PairCondor(x)).ToList();
		}
		#endregion
	}
}
