using System;

namespace Entities
{
	public class BearCallSpreadPropertyNames : PropertyNamesBase
	{
		public BearCallSpreadPropertyNames() : base(String.Empty) {}
		public BearCallSpreadPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Quantity { get { return ResolvePropertyName("Quantity"); } }
		public String SellStrike { get { return ResolvePropertyName("SellStrike"); } }
		public String BuyStrike { get { return ResolvePropertyName("BuyStrike"); } }
		public String SellCallIdentifier { get { return ResolvePropertyName("SellCallIdentifier"); } }
		public String SellCallInclude { get { return ResolvePropertyName("SellCall"); } }
		public CallPropertyNames SellCall { get { return new CallPropertyNames(ResolvePropertyName("SellCall")); } }
		public String BuyCallIdentifier { get { return ResolvePropertyName("BuyCallIdentifier"); } }
		public String BuyCallInclude { get { return ResolvePropertyName("BuyCall"); } }
		public CallPropertyNames BuyCall { get { return new CallPropertyNames(ResolvePropertyName("BuyCall")); } }
		public String SecurityIdentifier { get { return ResolvePropertyName("SecurityIdentifier"); } }
		public String SecurityInclude { get { return ResolvePropertyName("Security"); } }
		public SecurityPropertyNames Security { get { return new SecurityPropertyNames(ResolvePropertyName("Security")); } }
	}
}
