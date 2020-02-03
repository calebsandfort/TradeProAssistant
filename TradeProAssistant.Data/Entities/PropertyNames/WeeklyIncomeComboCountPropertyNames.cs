using System;

namespace Entities
{
	public class WeeklyIncomeComboCountPropertyNames : PropertyNamesBase
	{
		public WeeklyIncomeComboCountPropertyNames() : base(String.Empty) {}
		public WeeklyIncomeComboCountPropertyNames(String parent) : base(parent) {}

		public String Identifier { get { return ResolvePropertyName("Identifier"); } }
		public String Count { get { return ResolvePropertyName("Count"); } }

		public String SectorEnum { get { return ResolvePropertyName("SectorEnum"); } }
		public String PlaySheetIdentifier { get { return ResolvePropertyName("PlaySheetIdentifier"); } }
		public String PlaySheetInclude { get { return ResolvePropertyName("PlaySheet"); } }
		public WeeklyIncomePlaySheetPropertyNames PlaySheet { get { return new WeeklyIncomePlaySheetPropertyNames(ResolvePropertyName("PlaySheet")); } }
	}
}
