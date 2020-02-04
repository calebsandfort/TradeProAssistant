using System;
using Data.Framework;
namespace Enums
{
	public enum CondorTypes
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Pair")]
		Pair = 1,
		[StringValue("Iron")]
		Iron = 2,
	}

	public static class CondorTypesExtensions
	{
		public static CondorTypes ToCondorType(this String val)
		{
			CondorTypes retVal = CondorTypes.None;

			switch(val)
			{
				case "Pair":
					retVal = CondorTypes.Pair;
					break;
				case "Iron":
					retVal = CondorTypes.Iron;
					break;
			}

			return retVal;
		}
	}
}
