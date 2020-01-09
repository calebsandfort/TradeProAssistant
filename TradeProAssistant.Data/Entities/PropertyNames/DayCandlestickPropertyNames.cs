using System;

namespace Entities
{
	public class DayCandlestickPropertyNames : PropertyNamesBase
	{
		public DayCandlestickPropertyNames() : base(String.Empty) {}
		public DayCandlestickPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Date { get { return ResolvePropertyName("Date"); } }
		public String Open { get { return ResolvePropertyName("Open"); } }
		public String High { get { return ResolvePropertyName("High"); } }
		public String Low { get { return ResolvePropertyName("Low"); } }
		public String Close { get { return ResolvePropertyName("Close"); } }
		public String Volume { get { return ResolvePropertyName("Volume"); } }
	}
}
