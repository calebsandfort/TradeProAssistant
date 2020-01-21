using System;
using Data.Framework;
namespace Enums
{
	public enum WeeklyIncomeActions
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Set Important Dates")]
		SetImportantDates = 1,
		[StringValue("Download Option Chains")]
		DownloadOptionChains = 2,
		[StringValue("Generate Play Sheet")]
		GeneratePlaySheet = 3,
	}

	public static class WeeklyIncomeActionsExtensions
	{
		public static WeeklyIncomeActions ToWeeklyIncomeAction(this String val)
		{
			WeeklyIncomeActions retVal = WeeklyIncomeActions.None;

			switch(val)
			{
				case "Set Important Dates":
					retVal = WeeklyIncomeActions.SetImportantDates;
					break;
				case "Download Option Chains":
					retVal = WeeklyIncomeActions.DownloadOptionChains;
					break;
				case "Generate Play Sheet":
					retVal = WeeklyIncomeActions.GeneratePlaySheet;
					break;
			}

			return retVal;
		}
	}
}
