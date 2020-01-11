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
		public String DailyCandlesticksInclude { get { return ResolvePropertyName("DailyCandlesticks"); } }
		public DayCandlestickPropertyNames DailyCandlesticks { get { return new DayCandlestickPropertyNames(ResolvePropertyName("DailyCandlesticks")); } }
		public String WeeklyCandlesticksInclude { get { return ResolvePropertyName("WeeklyCandlesticks"); } }
		public WeekCandlestickPropertyNames WeeklyCandlesticks { get { return new WeekCandlestickPropertyNames(ResolvePropertyName("WeeklyCandlesticks")); } }
		public String OptionChainsInclude { get { return ResolvePropertyName("OptionChains"); } }
		public OptionChainPropertyNames OptionChains { get { return new OptionChainPropertyNames(ResolvePropertyName("OptionChains")); } }
	}
}
