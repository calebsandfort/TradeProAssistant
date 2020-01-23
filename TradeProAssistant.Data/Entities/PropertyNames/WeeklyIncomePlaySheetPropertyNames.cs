using System;

namespace Entities
{
	public class WeeklyIncomePlaySheetPropertyNames : PropertyNamesBase
	{
		public WeeklyIncomePlaySheetPropertyNames() : base(String.Empty) {}
		public WeeklyIncomePlaySheetPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String TimeStamp { get { return ResolvePropertyName("TimeStamp"); } }
		public String Expiry { get { return ResolvePropertyName("Expiry"); } }
		public String Used { get { return ResolvePropertyName("Used"); } }
		public String ActionPlansInclude { get { return ResolvePropertyName("ActionPlans"); } }
		public WeeklyIncomeActionPlanPropertyNames ActionPlans { get { return new WeeklyIncomeActionPlanPropertyNames(ResolvePropertyName("ActionPlans")); } }
		public String ComboCountsInclude { get { return ResolvePropertyName("ComboCounts"); } }
		public WeeklyIncomeComboCountPropertyNames ComboCounts { get { return new WeeklyIncomeComboCountPropertyNames(ResolvePropertyName("ComboCounts")); } }
	}
}
