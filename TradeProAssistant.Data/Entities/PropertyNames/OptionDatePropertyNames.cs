using System;

namespace Entities
{
	public class OptionDatePropertyNames : PropertyNamesBase
	{
		public OptionDatePropertyNames() : base(String.Empty) {}
		public OptionDatePropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String ExpiryDate { get { return ResolvePropertyName("ExpiryDate"); } }
		public String StrikesInclude { get { return ResolvePropertyName("Strikes"); } }
		public OptionStrikePropertyNames Strikes { get { return new OptionStrikePropertyNames(ResolvePropertyName("Strikes")); } }
	}
}
