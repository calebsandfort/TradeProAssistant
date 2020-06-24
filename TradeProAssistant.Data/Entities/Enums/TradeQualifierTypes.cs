using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum TradeQualifierTypes
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Market Correlations")]
		MarketCorrelations = 1,
		[StringValue("Inventory")]
		Inventory = 2,
		[StringValue("Footprint Charts")]
		FootprintCharts = 3,
		[StringValue("Misc")]
		Misc = 4,
		[StringValue("MarketStructure")]
		MarketStructure = 5,
	}

	public static class TradeQualifierTypesExtensions
	{
		public static TradeQualifierTypes ToTradeQualifierType(this String val)
		{
			TradeQualifierTypes retVal = TradeQualifierTypes.None;

			switch(val)
			{
				case "Market Correlations":
					retVal = TradeQualifierTypes.MarketCorrelations;
					break;
				case "Inventory":
					retVal = TradeQualifierTypes.Inventory;
					break;
				case "Footprint Charts":
					retVal = TradeQualifierTypes.FootprintCharts;
					break;
				case "Misc":
					retVal = TradeQualifierTypes.Misc;
					break;
				case "MarketStructure":
					retVal = TradeQualifierTypes.MarketStructure;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = TradeQualifierTypes.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Market Correlations", Value = TradeQualifierTypes.MarketCorrelations.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Inventory", Value = TradeQualifierTypes.Inventory.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Footprint Charts", Value = TradeQualifierTypes.FootprintCharts.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Misc", Value = TradeQualifierTypes.Misc.ToString() });
			selectItems.Add(new SelectListItem() { Text = "MarketStructure", Value = TradeQualifierTypes.MarketStructure.ToString() });
			return selectItems;
		}
	}
}
