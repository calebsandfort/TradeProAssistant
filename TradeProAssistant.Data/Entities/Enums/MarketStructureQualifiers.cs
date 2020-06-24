using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum MarketStructureQualifiers
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Higher High on Impulse")]
		HigherHighOnImpulse = 1,
		[StringValue("Higher Lows on Impulse")]
		HigherLowsOnImpulse = 2,
		[StringValue("Previous High is Support")]
		PreviousHighIsSupport = 3,
		[StringValue("Lower Low on Impulse")]
		LowerLowOnImpulse = 4,
		[StringValue("Lower Highs on Impulse")]
		LowerHighsOnImpulse = 5,
		[StringValue("Previous Low is Resistance")]
		PreviousLowIsResistance = 6,
		[StringValue("Price is at Key Resistance Level Above")]
		FadeTheRally1 = 7,
		[StringValue("Price is currently at new higher high on impulse move or at previous failed high")]
		FadeTheRally2 = 8,
		[StringValue("Price is at a key support level")]
		FadeTheDrop1 = 9,
		[StringValue("Price is currently at new lower low on impulse move or at previous failed low")]
		FadeTheDrop2 = 10,
		[StringValue("Price has just broken or is at top tick of previous swing high")]
		BuyTheBreakout1 = 11,
		[StringValue("This is the first, or second bullish impulse wave at most")]
		BuyTheBreakout2 = 12,
		[StringValue("Higher lows were formed on price movement up to current level")]
		BuyTheBreakout3 = 13,
		[StringValue("Price has just broken or is at low tick of previous swing low")]
		SellTheBreakout1 = 14,
		[StringValue("This is the first, or second bearish impulse wave at most")]
		SellTheBreakout2 = 15,
		[StringValue("Lower highs were formed on price movement down to current level")]
		SellTheBreakout3 = 16,
	}

	public static class MarketStructureQualifiersExtensions
	{
		public static MarketStructureQualifiers ToMarketStructureQualifier(this String val)
		{
			MarketStructureQualifiers retVal = MarketStructureQualifiers.None;

			switch(val)
			{
				case "Higher High on Impulse":
					retVal = MarketStructureQualifiers.HigherHighOnImpulse;
					break;
				case "Higher Lows on Impulse":
					retVal = MarketStructureQualifiers.HigherLowsOnImpulse;
					break;
				case "Previous High is Support":
					retVal = MarketStructureQualifiers.PreviousHighIsSupport;
					break;
				case "Lower Low on Impulse":
					retVal = MarketStructureQualifiers.LowerLowOnImpulse;
					break;
				case "Lower Highs on Impulse":
					retVal = MarketStructureQualifiers.LowerHighsOnImpulse;
					break;
				case "Previous Low is Resistance":
					retVal = MarketStructureQualifiers.PreviousLowIsResistance;
					break;
				case "Price is at Key Resistance Level Above":
					retVal = MarketStructureQualifiers.FadeTheRally1;
					break;
				case "Price is currently at new higher high on impulse move or at previous failed high":
					retVal = MarketStructureQualifiers.FadeTheRally2;
					break;
				case "Price is at a key support level":
					retVal = MarketStructureQualifiers.FadeTheDrop1;
					break;
				case "Price is currently at new lower low on impulse move or at previous failed low":
					retVal = MarketStructureQualifiers.FadeTheDrop2;
					break;
				case "Price has just broken or is at top tick of previous swing high":
					retVal = MarketStructureQualifiers.BuyTheBreakout1;
					break;
				case "This is the first, or second bullish impulse wave at most":
					retVal = MarketStructureQualifiers.BuyTheBreakout2;
					break;
				case "Higher lows were formed on price movement up to current level":
					retVal = MarketStructureQualifiers.BuyTheBreakout3;
					break;
				case "Price has just broken or is at low tick of previous swing low":
					retVal = MarketStructureQualifiers.SellTheBreakout1;
					break;
				case "This is the first, or second bearish impulse wave at most":
					retVal = MarketStructureQualifiers.SellTheBreakout2;
					break;
				case "Lower highs were formed on price movement down to current level":
					retVal = MarketStructureQualifiers.SellTheBreakout3;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = MarketStructureQualifiers.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Higher High on Impulse", Value = MarketStructureQualifiers.HigherHighOnImpulse.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Higher Lows on Impulse", Value = MarketStructureQualifiers.HigherLowsOnImpulse.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Previous High is Support", Value = MarketStructureQualifiers.PreviousHighIsSupport.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Lower Low on Impulse", Value = MarketStructureQualifiers.LowerLowOnImpulse.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Lower Highs on Impulse", Value = MarketStructureQualifiers.LowerHighsOnImpulse.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Previous Low is Resistance", Value = MarketStructureQualifiers.PreviousLowIsResistance.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Price is at Key Resistance Level Above", Value = MarketStructureQualifiers.FadeTheRally1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Price is currently at new higher high on impulse move or at previous failed high", Value = MarketStructureQualifiers.FadeTheRally2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Price is at a key support level", Value = MarketStructureQualifiers.FadeTheDrop1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Price is currently at new lower low on impulse move or at previous failed low", Value = MarketStructureQualifiers.FadeTheDrop2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Price has just broken or is at top tick of previous swing high", Value = MarketStructureQualifiers.BuyTheBreakout1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "This is the first, or second bullish impulse wave at most", Value = MarketStructureQualifiers.BuyTheBreakout2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Higher lows were formed on price movement up to current level", Value = MarketStructureQualifiers.BuyTheBreakout3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Price has just broken or is at low tick of previous swing low", Value = MarketStructureQualifiers.SellTheBreakout1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "This is the first, or second bearish impulse wave at most", Value = MarketStructureQualifiers.SellTheBreakout2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Lower highs were formed on price movement down to current level", Value = MarketStructureQualifiers.SellTheBreakout3.ToString() });
			return selectItems;
		}
	}
}
