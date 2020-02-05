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
		[StringValue("Generate Pair Condor Play Sheet")]
		GeneratePairCondorPlaySheet = 3,
		[StringValue("Set Benzinga IDs")]
		SetBenzingaIds = 4,
		[StringValue("Generate Iron Condor Play Sheet")]
		GenerateIronCondorPlaySheet = 5,
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
				case "Generate Pair Condor Play Sheet":
					retVal = WeeklyIncomeActions.GeneratePairCondorPlaySheet;
					break;
				case "Set Benzinga IDs":
					retVal = WeeklyIncomeActions.SetBenzingaIds;
					break;
				case "Generate Iron Condor Play Sheet":
					retVal = WeeklyIncomeActions.GenerateIronCondorPlaySheet;
					break;
			}

			return retVal;
		}
	}
}
