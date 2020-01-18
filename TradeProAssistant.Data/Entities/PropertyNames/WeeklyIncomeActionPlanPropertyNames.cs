using System;

namespace Entities
{
	public class WeeklyIncomeActionPlanPropertyNames : PropertyNamesBase
	{
		public WeeklyIncomeActionPlanPropertyNames() : base(String.Empty) {}
		public WeeklyIncomeActionPlanPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Dummy { get { return ResolvePropertyName("Dummy"); } }
		public String PlaySheetIdentifier { get { return ResolvePropertyName("PlaySheetIdentifier"); } }
		public String PlaySheetInclude { get { return ResolvePropertyName("PlaySheet"); } }
		public WeeklyIncomePlaySheetPropertyNames PlaySheet { get { return new WeeklyIncomePlaySheetPropertyNames(ResolvePropertyName("PlaySheet")); } }
		public String PairsInclude { get { return ResolvePropertyName("Pairs"); } }
		public PairCondorPropertyNames Pairs { get { return new PairCondorPropertyNames(ResolvePropertyName("Pairs")); } }
	}
}
