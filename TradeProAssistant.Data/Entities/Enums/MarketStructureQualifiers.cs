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
			return selectItems;
		}
	}
}
