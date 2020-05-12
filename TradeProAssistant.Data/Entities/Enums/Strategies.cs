using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum Strategies
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Buy the Dip")]
		BuyTheDip = 1,
		[StringValue("Sell the Rip")]
		SellTheRip = 2,
		[StringValue("Fade the Rally")]
		FadeTheRally = 3,
		[StringValue("Fade the Drop")]
		FadeTheDrop = 4,
		[StringValue("Buy the Breakout")]
		BuyTheBreakout = 5,
		[StringValue("Sell the Breakout")]
		SellTheBreakout = 6,
	}

	public static class StrategiesExtensions
	{
		public static Strategies ToStrategy(this String val)
		{
			Strategies retVal = Strategies.None;

			switch(val)
			{
				case "Buy the Dip":
					retVal = Strategies.BuyTheDip;
					break;
				case "Sell the Rip":
					retVal = Strategies.SellTheRip;
					break;
				case "Fade the Rally":
					retVal = Strategies.FadeTheRally;
					break;
				case "Fade the Drop":
					retVal = Strategies.FadeTheDrop;
					break;
				case "Buy the Breakout":
					retVal = Strategies.BuyTheBreakout;
					break;
				case "Sell the Breakout":
					retVal = Strategies.SellTheBreakout;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = Strategies.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Buy the Dip", Value = Strategies.BuyTheDip.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Sell the Rip", Value = Strategies.SellTheRip.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Fade the Rally", Value = Strategies.FadeTheRally.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Fade the Drop", Value = Strategies.FadeTheDrop.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Buy the Breakout", Value = Strategies.BuyTheBreakout.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Sell the Breakout", Value = Strategies.SellTheBreakout.ToString() });
			return selectItems;
		}
	}
}
