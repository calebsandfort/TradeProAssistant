using System;

namespace Entities
{
	public class RiskParametersPropertyNames : PropertyNamesBase
	{
		public RiskParametersPropertyNames() : base(String.Empty) {}
		public RiskParametersPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Name { get { return ResolvePropertyName("Name"); } }
		public String Active { get { return ResolvePropertyName("Active"); } }
		public String TpaDailyTarget { get { return ResolvePropertyName("TpaDailyTarget"); } }
		public String TpaWeeklyTarget { get { return ResolvePropertyName("TpaWeeklyTarget"); } }
		public String TpaMonthlyTarget { get { return ResolvePropertyName("TpaMonthlyTarget"); } }
		public String MyDailyTarget { get { return ResolvePropertyName("MyDailyTarget"); } }
		public String MyWeeklyTarget { get { return ResolvePropertyName("MyWeeklyTarget"); } }
		public String MyMonthlyTarget { get { return ResolvePropertyName("MyMonthlyTarget"); } }
		public String DailyStop { get { return ResolvePropertyName("DailyStop"); } }
		public String WeeklyStop { get { return ResolvePropertyName("WeeklyStop"); } }
		public String MonthlyStop { get { return ResolvePropertyName("MonthlyStop"); } }
	}
}
