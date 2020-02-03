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

		public bool Ignore { get; set; }

		public bool IsBullish { get; set; }

		public bool IsBearish { get; set; }

		public DateTime? ExDividendDate { get; set; }

		public DateTime? NextEarningsDate { get; set; }

		public Int32 BenzingaId { get; set; }

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

		public  Security(Security source)
		{
			this.Name = source.Name;
			this.Symbol = source.Symbol;
			this.MarketCap = source.MarketCap;
			this.CurrentPrice = source.CurrentPrice;
			this.Sector = source.Sector;
			this.AssetClass = source.AssetClass;
			this.PairEligible = source.PairEligible;
			this.Ignore = source.Ignore;
			this.IsBullish = source.IsBullish;
			this.IsBearish = source.IsBearish;
			this.ExDividendDate = source.ExDividendDate;
			this.NextEarningsDate = source.NextEarningsDate;
			this.BenzingaId = source.BenzingaId;
			this.SectorEnum = source.SectorEnum;
	this.AssetClassEnum = source.AssetClassEnum;
			this.DailyCandlesticks = source.DailyCandlesticks.Select(x => new DayCandlestick(x)).ToList();
			this.WeeklyCandlesticks = source.WeeklyCandlesticks.Select(x => new WeekCandlestick(x)).ToList();
			this.OptionChains = source.OptionChains.Select(x => new OptionChain(x)).ToList();
		}
		#endregion
	}
}
