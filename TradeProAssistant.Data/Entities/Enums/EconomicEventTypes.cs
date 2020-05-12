using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Enums
{
	public enum EconomicEventTypes
	{
		[StringValue("None")]
		None = 0,
		[StringValue("ISM Manufacturing PMI")]
		IsmManufacturingPmi = 1,
	}

	public static class EconomicEventTypesExtensions
	{
		public static EconomicEventTypes ToEconomicEventType(this String val)
		{
			EconomicEventTypes retVal = EconomicEventTypes.None;

			switch(val)
			{
				case "ISM Manufacturing PMI":
					retVal = EconomicEventTypes.IsmManufacturingPmi;
					break;
			}

			return retVal;
		}
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = EconomicEventTypes.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "ISM Manufacturing PMI", Value = EconomicEventTypes.IsmManufacturingPmi.ToString() });
			return selectItems;
		}
	}
}
