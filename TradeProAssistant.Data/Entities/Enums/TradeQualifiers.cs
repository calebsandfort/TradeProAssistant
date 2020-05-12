using System;
using Data.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
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
		[StringValue("Buy Imbalances Moving Higher")]
		BuyImbalancesMovingHigher = 7,
		[StringValue("Most Recent Sell Imbalance Below Current Price")]
		MostRecentSellImbalanceBelowCurrentPrice = 8,
		[StringValue("Large Sell Imbalance Near Lows of Prior Move")]
		LargeSellImbalanceNearLowsOfPriorMove = 9,
		[StringValue("Points of Control Below Market Price")]
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
		[StringValue("Sell Imbalances Moving Lower")]
		SellImbalancesMovingLower = 17,
		[StringValue("Most Recent Buy Imbalance Above Current Price")]
		MostRecentBuyImbalanceAboveCurrentPrice = 18,
		[StringValue("Large Buy Imbalance Near Highs of Prior Move")]
		LargeBuyImbalanceNearHighsOfPriorMove = 19,
		[StringValue("Points of Control Above Market Price")]
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
				case "Buy Imbalances Moving Higher":
					retVal = TradeQualifiers.BuyImbalancesMovingHigher;
					break;
				case "Most Recent Sell Imbalance Below Current Price":
					retVal = TradeQualifiers.MostRecentSellImbalanceBelowCurrentPrice;
					break;
				case "Large Sell Imbalance Near Lows of Prior Move":
					retVal = TradeQualifiers.LargeSellImbalanceNearLowsOfPriorMove;
					break;
				case "Points of Control Below Market Price":
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
				case "Sell Imbalances Moving Lower":
					retVal = TradeQualifiers.SellImbalancesMovingLower;
					break;
				case "Most Recent Buy Imbalance Above Current Price":
					retVal = TradeQualifiers.MostRecentBuyImbalanceAboveCurrentPrice;
					break;
				case "Large Buy Imbalance Near Highs of Prior Move":
					retVal = TradeQualifiers.LargeBuyImbalanceNearHighsOfPriorMove;
					break;
				case "Points of Control Above Market Price":
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
	

		public static IEnumerable<SelectListItem> SelectItems()
		{
			List<SelectListItem> selectItems = new List<SelectListItem>();
			selectItems.Add(new SelectListItem() { Text = "None", Value = TradeQualifiers.None.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Equity Markets Up", Value = TradeQualifiers.EquityMarketsUp.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Nasdaq Higher Faster than ES", Value = TradeQualifiers.NasdaqHigherFaster.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Risk Off Down", Value = TradeQualifiers.RiskOffDown.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Bids Moving Higher", Value = TradeQualifiers.BidsMovingHigher.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Offers Moving Higher or Pulled", Value = TradeQualifiers.OffersMovingHigherOrPulled.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Offer Magnet", Value = TradeQualifiers.OfferMagnet.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Buy Imbalances Moving Higher", Value = TradeQualifiers.BuyImbalancesMovingHigher.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Most Recent Sell Imbalance Below Current Price", Value = TradeQualifiers.MostRecentSellImbalanceBelowCurrentPrice.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Large Sell Imbalance Near Lows of Prior Move", Value = TradeQualifiers.LargeSellImbalanceNearLowsOfPriorMove.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Points of Control Below Market Price", Value = TradeQualifiers.PointsOfControlBelowMarketPrice.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Equity Markets Down", Value = TradeQualifiers.EquityMarketsDown.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Nasdaq Lower Faster than ES", Value = TradeQualifiers.NasdaqLowerFaster.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Risk Off Up", Value = TradeQualifiers.RiskOffUp.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Offers Moving Lower", Value = TradeQualifiers.OffersMovingLower.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Bids Moving Lower or Pulled", Value = TradeQualifiers.BidsMovingLowerOrPulled.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Bid Magnet", Value = TradeQualifiers.BidMagnet.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Sell Imbalances Moving Lower", Value = TradeQualifiers.SellImbalancesMovingLower.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Most Recent Buy Imbalance Above Current Price", Value = TradeQualifiers.MostRecentBuyImbalanceAboveCurrentPrice.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Large Buy Imbalance Near Highs of Prior Move", Value = TradeQualifiers.LargeBuyImbalanceNearHighsOfPriorMove.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Points of Control Above Market Price", Value = TradeQualifiers.PointsOfControlAboveMarketPrice.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Volume", Value = TradeQualifiers.Volume.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Pivot Points", Value = TradeQualifiers.PivotPoints.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Technicals", Value = TradeQualifiers.Technicals.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Market Structure", Value = TradeQualifiers.MarketStructure.ToString() });
			return selectItems;
		}
	}
}
