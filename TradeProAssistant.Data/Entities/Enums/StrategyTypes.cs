using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum StrategyTypes
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Pair Condor")]
		PairCondor = 1,
		[StringValue("Iron Condor")]
		IronCondor = 2,
	}

	public static class StrategyTypesExtensions
	{
		public static StrategyTypes ToStrategyType(this String val)
		{
			StrategyTypes retVal = StrategyTypes.None;

			switch(val)
			{
				case "Pair Condor":
					retVal = StrategyTypes.PairCondor;
					break;
				case "Iron Condor":
					retVal = StrategyTypes.IronCondor;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = StrategyTypes.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Pair Condor", Value = StrategyTypes.PairCondor.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Iron Condor", Value = StrategyTypes.IronCondor.ToString() });
			return selectItems;
		}
	}
}
