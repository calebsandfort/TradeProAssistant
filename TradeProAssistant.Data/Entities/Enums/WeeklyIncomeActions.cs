using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
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
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = WeeklyIncomeActions.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Set Important Dates", Value = WeeklyIncomeActions.SetImportantDates.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Download Option Chains", Value = WeeklyIncomeActions.DownloadOptionChains.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Generate Pair Condor Play Sheet", Value = WeeklyIncomeActions.GeneratePairCondorPlaySheet.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Set Benzinga IDs", Value = WeeklyIncomeActions.SetBenzingaIds.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Generate Iron Condor Play Sheet", Value = WeeklyIncomeActions.GenerateIronCondorPlaySheet.ToString() });
			return selectItems;
		}
	}
}
