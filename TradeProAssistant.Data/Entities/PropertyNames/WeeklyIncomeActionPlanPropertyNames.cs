using System;

namespace Entities
{
	public class WeeklyIncomeActionPlanPropertyNames : PropertyNamesBase
	{
		public WeeklyIncomeActionPlanPropertyNames() : base(String.Empty) {}
		public WeeklyIncomeActionPlanPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Credit { get { return ResolvePropertyName("Credit"); } }
		public String Risk { get { return ResolvePropertyName("Risk"); } }
		public String TimeStamp { get { return ResolvePropertyName("TimeStamp"); } }
		public String Expiry { get { return ResolvePropertyName("Expiry"); } }
		public String PairsInclude { get { return ResolvePropertyName("Pairs"); } }
		public PairCondorPropertyNames Pairs { get { return new PairCondorPropertyNames(ResolvePropertyName("Pairs")); } }
	}
}
