using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum AssetClasses
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Equity")]
		Equity = 1,
		[StringValue("ETF")]
		ETF = 2,
	}

	public static class AssetClassesExtensions
	{
		public static AssetClasses ToAssetClass(this String val)
		{
			AssetClasses retVal = AssetClasses.None;

			switch(val)
			{
				case "Equity":
					retVal = AssetClasses.Equity;
					break;
				case "ETF":
					retVal = AssetClasses.ETF;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = AssetClasses.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Equity", Value = AssetClasses.Equity.ToString() });
			selectItems.Add(new SelectListItem() { Text = "ETF", Value = AssetClasses.ETF.ToString() });
			return selectItems;
		}
	}
}
