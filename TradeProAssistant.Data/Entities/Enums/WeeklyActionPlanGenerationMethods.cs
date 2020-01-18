using System;
using Data.Framework;
namespace Enums
{
	public enum WeeklyActionPlanGenerationMethods
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Brute Force")]
		BruteForce = 1,
		[StringValue("RandomSearch")]
		RandomSearch = 2,
	}

	public static class WeeklyActionPlanGenerationMethodsExtensions
	{
		public static WeeklyActionPlanGenerationMethods ToWeeklyActionPlanGenerationMethod(this String val)
		{
			WeeklyActionPlanGenerationMethods retVal = WeeklyActionPlanGenerationMethods.None;

			switch(val)
			{
				case "Brute Force":
					retVal = WeeklyActionPlanGenerationMethods.BruteForce;
					break;
				case "RandomSearch":
					retVal = WeeklyActionPlanGenerationMethods.RandomSearch;
					break;
			}

			return retVal;
		}
	}
}
