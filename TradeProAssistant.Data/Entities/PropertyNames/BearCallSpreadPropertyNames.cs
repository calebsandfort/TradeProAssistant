using System;

namespace Entities
{
	public class BearCallSpreadPropertyNames : PropertyNamesBase
	{
		public BearCallSpreadPropertyNames() : base(String.Empty) {}
		public BearCallSpreadPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Credit { get { return ResolvePropertyName("Credit"); } }
		public String Risk { get { return ResolvePropertyName("Risk"); } }
		public String Quantity { get { return ResolvePropertyName("Quantity"); } }
		public String SellStrike { get { return ResolvePropertyName("SellStrike"); } }
		public String BuyStrike { get { return ResolvePropertyName("BuyStrike"); } }
		public String SecurityIdentifier { get { return ResolvePropertyName("SecurityIdentifier"); } }
		public String SecurityInclude { get { return ResolvePropertyName("Security"); } }
		public SecurityPropertyNames Security { get { return new SecurityPropertyNames(ResolvePropertyName("Security")); } }
	}
}
