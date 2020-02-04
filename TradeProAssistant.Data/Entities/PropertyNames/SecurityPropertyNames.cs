using System;

namespace Entities
{
	public class SecurityPropertyNames : PropertyNamesBase
	{
		public SecurityPropertyNames() : base(String.Empty) {}
		public SecurityPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Name { get { return ResolvePropertyName("Name"); } }
		public String Symbol { get { return ResolvePropertyName("Symbol"); } }
		public String MarketCap { get { return ResolvePropertyName("MarketCap"); } }
		public String CurrentPrice { get { return ResolvePropertyName("CurrentPrice"); } }
		public String Sector { get { return ResolvePropertyName("Sector"); } }
		public String AssetClass { get { return ResolvePropertyName("AssetClass"); } }
		public String PairEligible { get { return ResolvePropertyName("PairEligible"); } }
		public String Ignore { get { return ResolvePropertyName("Ignore"); } }
		public String IsBullish { get { return ResolvePropertyName("IsBullish"); } }
		public String IsBearish { get { return ResolvePropertyName("IsBearish"); } }
		public String ExDividendDate { get { return ResolvePropertyName("ExDividendDate"); } }
		public String NextEarningsDate { get { return ResolvePropertyName("NextEarningsDate"); } }
		public String BenzingaId { get { return ResolvePropertyName("BenzingaId"); } }
		public String Support { get { return ResolvePropertyName("Support"); } }
		public String Resistance { get { return ResolvePropertyName("Resistance"); } }
		public String IronCondorEligible { get { return ResolvePropertyName("IronCondorEligible"); } }

		public String SectorEnum { get { return ResolvePropertyName("SectorEnum"); } }

		public String AssetClassEnum { get { return ResolvePropertyName("AssetClassEnum"); } }
		public String DailyCandlesticksInclude { get { return ResolvePropertyName("DailyCandlesticks"); } }
		public DayCandlestickPropertyNames DailyCandlesticks { get { return new DayCandlestickPropertyNames(ResolvePropertyName("DailyCandlesticks")); } }
		public String WeeklyCandlesticksInclude { get { return ResolvePropertyName("WeeklyCandlesticks"); } }
		public WeekCandlestickPropertyNames WeeklyCandlesticks { get { return new WeekCandlestickPropertyNames(ResolvePropertyName("WeeklyCandlesticks")); } }
		public String OptionChainsInclude { get { return ResolvePropertyName("OptionChains"); } }
		public OptionChainPropertyNames OptionChains { get { return new OptionChainPropertyNames(ResolvePropertyName("OptionChains")); } }
	}
}
