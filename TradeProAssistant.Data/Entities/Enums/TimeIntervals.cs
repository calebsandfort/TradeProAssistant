using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum TimeIntervals
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Day")]
		Day = 1,
		[StringValue("Week")]
		Week = 2,
		[StringValue("Month")]
		Month = 3,
	}

	public static class TimeIntervalsExtensions
	{
		public static TimeIntervals ToTimeInterval(this String val)
		{
			TimeIntervals retVal = TimeIntervals.None;

			switch(val)
			{
				case "Day":
					retVal = TimeIntervals.Day;
					break;
				case "Week":
					retVal = TimeIntervals.Week;
					break;
				case "Month":
					retVal = TimeIntervals.Month;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = TimeIntervals.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Day", Value = TimeIntervals.Day.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Week", Value = TimeIntervals.Week.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Month", Value = TimeIntervals.Month.ToString() });
			return selectItems;
		}
	}
}
