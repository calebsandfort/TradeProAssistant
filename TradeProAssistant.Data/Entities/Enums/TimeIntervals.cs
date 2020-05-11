using System;
using Data.Framework;
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
	}
}
