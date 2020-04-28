using System;

namespace Entities
{
	public class CandlestickPropertyNames : PropertyNamesBase
	{
		public CandlestickPropertyNames() : base(String.Empty) {}
		public CandlestickPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Timestamp { get { return ResolvePropertyName("Timestamp"); } }
		public String Open { get { return ResolvePropertyName("Open"); } }
		public String High { get { return ResolvePropertyName("High"); } }
		public String Low { get { return ResolvePropertyName("Low"); } }
		public String Close { get { return ResolvePropertyName("Close"); } }
		public String Volume { get { return ResolvePropertyName("Volume"); } }

		public String Interval { get { return ResolvePropertyName("Interval"); } }
		public String FutureContractIdentifier { get { return ResolvePropertyName("FutureContractIdentifier"); } }
		public String FutureContractInclude { get { return ResolvePropertyName("FutureContract"); } }
		public FutureContractPropertyNames FutureContract { get { return new FutureContractPropertyNames(ResolvePropertyName("FutureContract")); } }
	}
}
