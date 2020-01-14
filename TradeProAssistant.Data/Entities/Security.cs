using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace Entities
{
	public partial class Security
	{
		[NotMapped]
		public static SecurityPropertyNames PropertyNames = new SecurityPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		[MaxLength(255)]
		public String Name { get; set; }

		[MaxLength(5)]
		public String Symbol { get; set; }

		public Decimal MarketCap { get; set; }

		public Decimal CurrentPrice { get; set; }

		[MaxLength(50)]
		public String Sector { get; set; }

		[MaxLength(50)]
		public String AssetClass { get; set; }

		public bool PairEligible { get; set; }

		public DateTime? ExDividendDate { get; set; }

		public DateTime? NextEarningsDate { get; set; }

		public Sectors SectorEnum { get; set; }

		public AssetClasses AssetClassEnum { get; set; }

		public virtual List<DayCandlestick> DailyCandlesticks { get; set; }
		public virtual List<WeekCandlestick> WeeklyCandlesticks { get; set; }
		public virtual List<OptionChain> OptionChains { get; set; }

		#region Constructor
		public  Security()
		{
			DailyCandlesticks = new List<DayCandlestick>();
	WeeklyCandlesticks = new List<WeekCandlestick>();
	OptionChains = new List<OptionChain>();
		}
		#endregion
	}
}
