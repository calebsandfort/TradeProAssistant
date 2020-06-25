using System;

namespace Entities
{
	public class TradingSettingsPropertyNames : PropertyNamesBase
	{
		public TradingSettingsPropertyNames() : base(String.Empty) {}
		public TradingSettingsPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String MonthStart { get { return ResolvePropertyName("MonthStart"); } }
		public String Weeks { get { return ResolvePropertyName("Weeks"); } }
		public String RiskParametersIdentifier { get { return ResolvePropertyName("RiskParametersIdentifier"); } }
		public String RiskParametersInclude { get { return ResolvePropertyName("RiskParameters"); } }
		public RiskParametersPropertyNames RiskParameters { get { return new RiskParametersPropertyNames(ResolvePropertyName("RiskParameters")); } }
	}
}
