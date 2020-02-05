using System;
using Data.Framework;
namespace Enums
{
	public enum StrategyTypes
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Pair Condor")]
		PairCondor = 1,
		[StringValue("Iron Condor")]
		IronCondor = 2,
	}

	public static class StrategyTypesExtensions
	{
		public static StrategyTypes ToStrategyType(this String val)
		{
			StrategyTypes retVal = StrategyTypes.None;

			switch(val)
			{
				case "Pair Condor":
					retVal = StrategyTypes.PairCondor;
					break;
				case "Iron Condor":
					retVal = StrategyTypes.IronCondor;
					break;
			}

			return retVal;
		}
	}
}
