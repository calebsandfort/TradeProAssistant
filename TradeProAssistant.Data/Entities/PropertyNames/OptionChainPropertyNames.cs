using System;

namespace Entities
{
	public class OptionChainPropertyNames : PropertyNamesBase
	{
		public OptionChainPropertyNames() : base(String.Empty) {}
		public OptionChainPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Date { get { return ResolvePropertyName("Date"); } }
		public String SecurityIdentifier { get { return ResolvePropertyName("SecurityIdentifier"); } }
		public String SecurityInclude { get { return ResolvePropertyName("Security"); } }
		public SecurityPropertyNames Security { get { return new SecurityPropertyNames(ResolvePropertyName("Security")); } }
		public String DatesInclude { get { return ResolvePropertyName("Dates"); } }
		public OptionDatePropertyNames Dates { get { return new OptionDatePropertyNames(ResolvePropertyName("Dates")); } }
	}
}
