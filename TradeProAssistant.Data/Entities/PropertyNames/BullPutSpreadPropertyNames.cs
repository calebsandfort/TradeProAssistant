using System;

namespace Entities
{
	public class BullPutSpreadPropertyNames : PropertyNamesBase
	{
		public BullPutSpreadPropertyNames() : base(String.Empty) {}
		public BullPutSpreadPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Quantity { get { return ResolvePropertyName("Quantity"); } }
		public String SellStrike { get { return ResolvePropertyName("SellStrike"); } }
		public String BuyStrike { get { return ResolvePropertyName("BuyStrike"); } }
		public String SellPutIdentifier { get { return ResolvePropertyName("SellPutIdentifier"); } }
		public String SellPutInclude { get { return ResolvePropertyName("SellPut"); } }
		public PutPropertyNames SellPut { get { return new PutPropertyNames(ResolvePropertyName("SellPut")); } }
		public String BuyPutIdentifier { get { return ResolvePropertyName("BuyPutIdentifier"); } }
		public String BuyPutInclude { get { return ResolvePropertyName("BuyPut"); } }
		public PutPropertyNames BuyPut { get { return new PutPropertyNames(ResolvePropertyName("BuyPut")); } }
		public String SecurityIdentifier { get { return ResolvePropertyName("SecurityIdentifier"); } }
		public String SecurityInclude { get { return ResolvePropertyName("Security"); } }
		public SecurityPropertyNames Security { get { return new SecurityPropertyNames(ResolvePropertyName("Security")); } }
	}
}
