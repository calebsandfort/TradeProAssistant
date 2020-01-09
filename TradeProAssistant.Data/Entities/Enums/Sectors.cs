using System;
using Data.Framework;
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
	}
}
