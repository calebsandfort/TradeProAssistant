using System;
using Data.Framework;
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
	}
}
