using System;
using Data.Framework;
namespace Enums
{
	public enum TradeProAssets
	{
		[StringValue("None")]
		None = 0,
		[StringValue("ES")]
		ES = 1,
		[StringValue("YM")]
		YM = 2,
	}

	public static class TradeProAssetsExtensions
	{
		public static TradeProAssets ToTradeProAsset(this String val)
		{
			TradeProAssets retVal = TradeProAssets.None;

			switch(val)
			{
				case "ES":
					retVal = TradeProAssets.ES;
					break;
				case "YM":
					retVal = TradeProAssets.YM;
					break;
			}

			return retVal;
		}
	}
}
