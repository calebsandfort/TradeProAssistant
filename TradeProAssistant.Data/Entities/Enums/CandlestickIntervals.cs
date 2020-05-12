using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum CandlestickIntervals
	{
		[StringValue("None")]
		None = 0,
		[StringValue("1 Minute")]
		OneMinute = 1,
		[StringValue("3 Minutes")]
		ThreeMinutes = 3,
		[StringValue("5 Minutes")]
		FiveMinutes = 5,
		[StringValue("10 Minutes")]
		TenMinutes = 10,
	}

	public static class CandlestickIntervalsExtensions
	{
		public static CandlestickIntervals ToCandlestickInterval(this String val)
		{
			CandlestickIntervals retVal = CandlestickIntervals.None;

			switch(val)
			{
				case "1 Minute":
					retVal = CandlestickIntervals.OneMinute;
					break;
				case "3 Minutes":
					retVal = CandlestickIntervals.ThreeMinutes;
					break;
				case "5 Minutes":
					retVal = CandlestickIntervals.FiveMinutes;
					break;
				case "10 Minutes":
					retVal = CandlestickIntervals.TenMinutes;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = CandlestickIntervals.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "1 Minute", Value = CandlestickIntervals.OneMinute.ToString() });
			selectItems.Add(new SelectListItem() { Text = "3 Minutes", Value = CandlestickIntervals.ThreeMinutes.ToString() });
			selectItems.Add(new SelectListItem() { Text = "5 Minutes", Value = CandlestickIntervals.FiveMinutes.ToString() });
			selectItems.Add(new SelectListItem() { Text = "10 Minutes", Value = CandlestickIntervals.TenMinutes.ToString() });
			return selectItems;
		}
	}
}
