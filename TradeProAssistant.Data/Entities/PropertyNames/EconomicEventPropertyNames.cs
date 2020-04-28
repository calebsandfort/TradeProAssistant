using System;

namespace Entities
{
	public class EconomicEventPropertyNames : PropertyNamesBase
	{
		public EconomicEventPropertyNames() : base(String.Empty) {}
		public EconomicEventPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Timestamp { get { return ResolvePropertyName("Timestamp"); } }

		public String EventType { get { return ResolvePropertyName("EventType"); } }
		public String EconomicDayIdentifier { get { return ResolvePropertyName("EconomicDayIdentifier"); } }
		public String EconomicDayInclude { get { return ResolvePropertyName("EconomicDay"); } }
		public EconomicDayPropertyNames EconomicDay { get { return new EconomicDayPropertyNames(ResolvePropertyName("EconomicDay")); } }
	}
}
