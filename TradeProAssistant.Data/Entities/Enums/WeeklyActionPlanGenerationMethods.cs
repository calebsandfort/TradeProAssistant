using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum WeeklyActionPlanGenerationMethods
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Brute Force")]
		BruteForce = 1,
		[StringValue("Random Search")]
		RandomSearch = 2,
		[StringValue("Genetic Optimization")]
		GeneticOptimization = 3,
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
				case "Random Search":
					retVal = WeeklyActionPlanGenerationMethods.RandomSearch;
					break;
				case "Genetic Optimization":
					retVal = WeeklyActionPlanGenerationMethods.GeneticOptimization;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = WeeklyActionPlanGenerationMethods.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Brute Force", Value = WeeklyActionPlanGenerationMethods.BruteForce.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Random Search", Value = WeeklyActionPlanGenerationMethods.RandomSearch.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Genetic Optimization", Value = WeeklyActionPlanGenerationMethods.GeneticOptimization.ToString() });
			return selectItems;
		}
	}
}
