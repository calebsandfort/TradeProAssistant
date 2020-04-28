using System;
using Data.Framework;
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
	}
}
