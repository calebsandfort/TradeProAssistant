using System;
using Data.Framework;
namespace Enums
{
	public enum AssetClasses
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Equity")]
		Equity = 1,
		[StringValue("ETF")]
		ETF = 2,
	}

	public static class AssetClassesExtensions
	{
		public static AssetClasses ToAssetClass(this String val)
		{
			AssetClasses retVal = AssetClasses.None;

			switch(val)
			{
				case "Equity":
					retVal = AssetClasses.Equity;
					break;
				case "ETF":
					retVal = AssetClasses.ETF;
					break;
			}

			return retVal;
		}
	}
}
