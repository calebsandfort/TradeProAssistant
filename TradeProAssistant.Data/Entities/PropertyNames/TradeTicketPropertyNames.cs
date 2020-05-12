using System;

namespace Entities
{
	public class TradeTicketPropertyNames : PropertyNamesBase
	{
		public TradeTicketPropertyNames() : base(String.Empty) {}
		public TradeTicketPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Timestamp { get { return ResolvePropertyName("Timestamp"); } }
		public String ZoneQualified { get { return ResolvePropertyName("ZoneQualified"); } }
		public String Qualifier1Disqualified { get { return ResolvePropertyName("Qualifier1Disqualified"); } }
		public String Qualifier2Disqualified { get { return ResolvePropertyName("Qualifier2Disqualified"); } }
		public String Qualifier3Disqualified { get { return ResolvePropertyName("Qualifier3Disqualified"); } }
		public String Qualifier4Disqualified { get { return ResolvePropertyName("Qualifier4Disqualified"); } }
		public String Notes { get { return ResolvePropertyName("Notes"); } }
		public String Won { get { return ResolvePropertyName("Won"); } }
		public String Scratch { get { return ResolvePropertyName("Scratch"); } }
		public String PnL { get { return ResolvePropertyName("PnL"); } }
		public String Quantity { get { return ResolvePropertyName("Quantity"); } }

		public String Asset { get { return ResolvePropertyName("Asset"); } }

		public String Strategy { get { return ResolvePropertyName("Strategy"); } }

		public String Qualifier1 { get { return ResolvePropertyName("Qualifier1"); } }

		public String Qualifier2 { get { return ResolvePropertyName("Qualifier2"); } }

		public String Qualifier3 { get { return ResolvePropertyName("Qualifier3"); } }

		public String Qualifier4 { get { return ResolvePropertyName("Qualifier4"); } }
	}
}
