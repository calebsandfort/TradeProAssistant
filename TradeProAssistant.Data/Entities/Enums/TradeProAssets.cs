using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum TradeProAssets
	{
		[StringValue("None")]
		None = 0,
		[StringValue("ES")]
		ES = 1,
		[StringValue("YM")]
		YM = 2,
	}

	public static class TradeProAssetsExtensions
	{
		public static TradeProAssets ToTradeProAsset(this String val)
		{
			TradeProAssets retVal = TradeProAssets.None;

			switch(val)
			{
				case "ES":
					retVal = TradeProAssets.ES;
					break;
				case "YM":
					retVal = TradeProAssets.YM;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = TradeProAssets.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "ES", Value = TradeProAssets.ES.ToString() });
			selectItems.Add(new SelectListItem() { Text = "YM", Value = TradeProAssets.YM.ToString() });
			return selectItems;
		}
	}
}
