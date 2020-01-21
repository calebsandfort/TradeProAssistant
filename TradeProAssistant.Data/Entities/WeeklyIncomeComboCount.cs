using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace Entities
{
	public partial class WeeklyIncomeComboCount
	{
		[NotMapped]
		public static WeeklyIncomeComboCountPropertyNames PropertyNames = new WeeklyIncomeComboCountPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		public Int32 Count { get; set; }

		public Sectors SectorEnum { get; set; }

		public int? PlaySheetIdentifier { get; set; }
		public virtual WeeklyIncomePlaySheet PlaySheet { get; set; }

		#region Constructor
		public  WeeklyIncomeComboCount()
		{
				}

		public  WeeklyIncomeComboCount(WeeklyIncomeComboCount source)
		{
			this.Count = source.Count;
			this.SectorEnum = source.SectorEnum;
			if(source.PlaySheet != null) this.PlaySheet = new WeeklyIncomePlaySheet(source.PlaySheet);
		}
		#endregion
	}
}
