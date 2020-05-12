using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum Sectors
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Consumer Discretionary")]
		ConsumerDiscretionary = 1,
		[StringValue("Consumer Staples")]
		ConsumerStaples = 2,
		[StringValue("Utilities")]
		Utilities = 3,
		[StringValue("Cash and/or Derivatives")]
		CashAndOrDerivatives = 4,
		[StringValue("Materials")]
		Materials = 5,
		[StringValue("Financials")]
		Financials = 6,
		[StringValue("Real Estate")]
		RealEstate = 7,
		[StringValue("Energy")]
		Energy = 8,
		[StringValue("Health Care")]
		HealthCare = 9,
		[StringValue("Industrials")]
		Industrials = 10,
		[StringValue("Information Technology")]
		InformationTechnology = 11,
		[StringValue("Communication")]
		Communication = 12,
		[StringValue("Technology")]
		Technology = 13,
		[StringValue("Index")]
		Index = 14,
	}

	public static class SectorsExtensions
	{
		public static Sectors ToSector(this String val)
		{
			Sectors retVal = Sectors.None;

			switch(val)
			{
				case "Consumer Discretionary":
					retVal = Sectors.ConsumerDiscretionary;
					break;
				case "Consumer Staples":
					retVal = Sectors.ConsumerStaples;
					break;
				case "Utilities":
					retVal = Sectors.Utilities;
					break;
				case "Cash and/or Derivatives":
					retVal = Sectors.CashAndOrDerivatives;
					break;
				case "Materials":
					retVal = Sectors.Materials;
					break;
				case "Financials":
					retVal = Sectors.Financials;
					break;
				case "Real Estate":
					retVal = Sectors.RealEstate;
					break;
				case "Energy":
					retVal = Sectors.Energy;
					break;
				case "Health Care":
					retVal = Sectors.HealthCare;
					break;
				case "Industrials":
					retVal = Sectors.Industrials;
					break;
				case "Information Technology":
					retVal = Sectors.InformationTechnology;
					break;
				case "Communication":
					retVal = Sectors.Communication;
					break;
				case "Technology":
					retVal = Sectors.Technology;
					break;
				case "Index":
					retVal = Sectors.Index;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = Sectors.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Consumer Discretionary", Value = Sectors.ConsumerDiscretionary.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Consumer Staples", Value = Sectors.ConsumerStaples.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Utilities", Value = Sectors.Utilities.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Cash and/or Derivatives", Value = Sectors.CashAndOrDerivatives.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Materials", Value = Sectors.Materials.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Financials", Value = Sectors.Financials.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Real Estate", Value = Sectors.RealEstate.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Energy", Value = Sectors.Energy.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Health Care", Value = Sectors.HealthCare.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Industrials", Value = Sectors.Industrials.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Information Technology", Value = Sectors.InformationTechnology.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Communication", Value = Sectors.Communication.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Technology", Value = Sectors.Technology.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Index", Value = Sectors.Index.ToString() });
			return selectItems;
		}
	}
}
