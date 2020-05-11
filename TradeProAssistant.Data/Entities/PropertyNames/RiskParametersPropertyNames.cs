using System;

namespace Entities
{
    public class RiskParametersPropertyNames : PropertyNamesBase
    {
        public RiskParametersPropertyNames() : base(String.Empty) { }
        public RiskParametersPropertyNames(String parent) : base(parent) { }

        public String Identifier { get { return ResolvePropertyName("Identifier"); } }
        public String Name { get { return ResolvePropertyName("Name"); } }
        public String Active { get { return ResolvePropertyName("Active"); } }
        public String DailyTarget { get { return ResolvePropertyName("DailyTarget"); } }
        public String WeeklyTarget { get { return ResolvePropertyName("WeeklyTarget"); } }
        public String MonthlyTarget { get { return ResolvePropertyName("MonthlyTarget"); } }
        public String DailyStop { get { return ResolvePropertyName("DailyStop"); } }
        public String WeeklyStop { get { return ResolvePropertyName("WeeklyStop"); } }
        public String MonthlyStop { get { return ResolvePropertyName("MonthlyStop"); } }
    }
}