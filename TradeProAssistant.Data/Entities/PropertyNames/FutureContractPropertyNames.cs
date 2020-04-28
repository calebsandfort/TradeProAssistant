using System;

namespace Entities
{
	public class FutureContractPropertyNames : PropertyNamesBase
	{
		public FutureContractPropertyNames() : base(String.Empty) {}
		public FutureContractPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Name { get { return ResolvePropertyName("Name"); } }
		public String Symbol { get { return ResolvePropertyName("Symbol"); } }
		public String TickValue { get { return ResolvePropertyName("TickValue"); } }
		public String TickSize { get { return ResolvePropertyName("TickSize"); } }
		public String CandlesticksInclude { get { return ResolvePropertyName("Candlesticks"); } }
		public CandlestickPropertyNames Candlesticks { get { return new CandlestickPropertyNames(ResolvePropertyName("Candlesticks")); } }
	}
}
