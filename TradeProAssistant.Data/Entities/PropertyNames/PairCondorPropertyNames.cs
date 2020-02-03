using System;

namespace Entities
{
	public class PairCondorPropertyNames : PropertyNamesBase
	{
		public PairCondorPropertyNames() : base(String.Empty) {}
		public PairCondorPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Dummy { get { return ResolvePropertyName("Dummy"); } }

		public String SectorEnum { get { return ResolvePropertyName("SectorEnum"); } }
		public String BullPutSpreadIdentifier { get { return ResolvePropertyName("BullPutSpreadIdentifier"); } }
		public String BullPutSpreadInclude { get { return ResolvePropertyName("BullPutSpread"); } }
		public BullPutSpreadPropertyNames BullPutSpread { get { return new BullPutSpreadPropertyNames(ResolvePropertyName("BullPutSpread")); } }
		public String BearCallSpreadIdentifier { get { return ResolvePropertyName("BearCallSpreadIdentifier"); } }
		public String BearCallSpreadInclude { get { return ResolvePropertyName("BearCallSpread"); } }
		public BearCallSpreadPropertyNames BearCallSpread { get { return new BearCallSpreadPropertyNames(ResolvePropertyName("BearCallSpread")); } }
	}
}
