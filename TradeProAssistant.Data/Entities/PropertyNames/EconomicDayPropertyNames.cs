using System;

namespace Entities
{
	public class EconomicDayPropertyNames : PropertyNamesBase
	{
		public EconomicDayPropertyNames() : base(String.Empty) {}
		public EconomicDayPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Date { get { return ResolvePropertyName("Date"); } }
		public String EventsInclude { get { return ResolvePropertyName("Events"); } }
		public EconomicEventPropertyNames Events { get { return new EconomicEventPropertyNames(ResolvePropertyName("Events")); } }
	}
}
