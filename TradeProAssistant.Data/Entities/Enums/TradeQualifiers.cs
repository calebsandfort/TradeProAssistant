using System;
using Data.Framework;
namespace Enums
{
	public enum TradeQualifiers
	{
		[StringValue("None")]
		None = 0,
		[StringValue("Equity Markets Up")]
		EquityMarketsUp = 1,
		[StringValue("Nasdaq Higher Faster than ES")]
		NasdaqHigherFaster = 2,
		[StringValue("Risk Off Down")]
		RiskOffDown = 3,
		[StringValue("Bids Moving Higher")]
		BidsMovingHigher = 4,
		[StringValue("Offers Moving Higher or Pulled")]
		OffersMovingHigherOrPulled = 5,
		[StringValue("Offer Magnet")]
		OfferMagnet = 6,
		[StringValue("BuyImbalancesMovingHigher")]
		BuyImbalancesMovingHigher = 7,
		[StringValue("Most Recent Sell Imbalance Below Current Price")]
		MostRecentSellImbalanceBelowCurrentPrice = 8,
		[StringValue("Large Sell Imbalance Near Lows of Prior Move")]
		LargeSellImbalanceNearLowsOfPriorMove = 9,
		[StringValue("PointsOfControlBelowMarketPrice")]
		PointsOfControlBelowMarketPrice = 10,
		[StringValue("Equity Markets Down")]
		EquityMarketsDown = 11,
		[StringValue("Nasdaq Lower Faster than ES")]
		NasdaqLowerFaster = 12,
		[StringValue("Risk Off Up")]
		RiskOffUp = 13,
		[StringValue("Offers Moving Lower")]
		OffersMovingLower = 14,
		[StringValue("Bids Moving Lower or Pulled")]
		BidsMovingLowerOrPulled = 15,
		[StringValue("Bid Magnet")]
		BidMagnet = 16,
		[StringValue("SellImbalancesMovingLower")]
		SellImbalancesMovingLower = 17,
		[StringValue("Most Recent Buy Imbalance Above Current Price")]
		MostRecentBuyImbalanceAboveCurrentPrice = 18,
		[StringValue("Large Buy Imbalance Near Highs of Prior Move")]
		LargeBuyImbalanceNearHighsOfPriorMove = 19,
		[StringValue("PointsOfControlAboveMarketPrice")]
		PointsOfControlAboveMarketPrice = 20,
		[StringValue("Volume")]
		Volume = 80,
		[StringValue("Pivot Points")]
		PivotPoints = 81,
		[StringValue("Technicals")]
		Technicals = 82,
		[StringValue("Market Structure")]
		MarketStructure = 83,
	}

	public static class TradeQualifiersExtensions
	{
		public static TradeQualifiers ToTradeQualifier(this String val)
		{
			TradeQualifiers retVal = TradeQualifiers.None;

			switch(val)
			{
				case "Equity Markets Up":
					retVal = TradeQualifiers.EquityMarketsUp;
					break;
				case "Nasdaq Higher Faster than ES":
					retVal = TradeQualifiers.NasdaqHigherFaster;
					break;
				case "Risk Off Down":
					retVal = TradeQualifiers.RiskOffDown;
					break;
				case "Bids Moving Higher":
					retVal = TradeQualifiers.BidsMovingHigher;
					break;
				case "Offers Moving Higher or Pulled":
					retVal = TradeQualifiers.OffersMovingHigherOrPulled;
					break;
				case "Offer Magnet":
					retVal = TradeQualifiers.OfferMagnet;
					break;
				case "BuyImbalancesMovingHigher":
					retVal = TradeQualifiers.BuyImbalancesMovingHigher;
					break;
				case "Most Recent Sell Imbalance Below Current Price":
					retVal = TradeQualifiers.MostRecentSellImbalanceBelowCurrentPrice;
					break;
				case "Large Sell Imbalance Near Lows of Prior Move":
					retVal = TradeQualifiers.LargeSellImbalanceNearLowsOfPriorMove;
					break;
				case "PointsOfControlBelowMarketPrice":
					retVal = TradeQualifiers.PointsOfControlBelowMarketPrice;
					break;
				case "Equity Markets Down":
					retVal = TradeQualifiers.EquityMarketsDown;
					break;
				case "Nasdaq Lower Faster than ES":
					retVal = TradeQualifiers.NasdaqLowerFaster;
					break;
				case "Risk Off Up":
					retVal = TradeQualifiers.RiskOffUp;
					break;
				case "Offers Moving Lower":
					retVal = TradeQualifiers.OffersMovingLower;
					break;
				case "Bids Moving Lower or Pulled":
					retVal = TradeQualifiers.BidsMovingLowerOrPulled;
					break;
				case "Bid Magnet":
					retVal = TradeQualifiers.BidMagnet;
					break;
				case "SellImbalancesMovingLower":
					retVal = TradeQualifiers.SellImbalancesMovingLower;
					break;
				case "Most Recent Buy Imbalance Above Current Price":
					retVal = TradeQualifiers.MostRecentBuyImbalanceAboveCurrentPrice;
					break;
				case "Large Buy Imbalance Near Highs of Prior Move":
					retVal = TradeQualifiers.LargeBuyImbalanceNearHighsOfPriorMove;
					break;
				case "PointsOfControlAboveMarketPrice":
					retVal = TradeQualifiers.PointsOfControlAboveMarketPrice;
					break;
				case "Volume":
					retVal = TradeQualifiers.Volume;
					break;
				case "Pivot Points":
					retVal = TradeQualifiers.PivotPoints;
					break;
				case "Technicals":
					retVal = TradeQualifiers.Technicals;
					break;
				case "Market Structure":
					retVal = TradeQualifiers.MarketStructure;
					break;
			}

			return retVal;
		}
	}
}
