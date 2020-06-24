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
		[StringValue("Equity markets sideways after big rally higher")]
		FadeTheRallyMC1 = 100,
		[StringValue("Nasdaq is higher than ES and moving sideways, or lower")]
		FadeTheRallyMC2 = 101,
		[StringValue("Risk off sideways near lows or starting to move up")]
		FadeTheRallyMC3 = 102,
		[StringValue("Offers are moving lower and closer to market price")]
		FadeTheRallyI1 = 103,
		[StringValue("Bids have stopped moving higher with the trend")]
		FadeTheRallyI2 = 104,
		[StringValue("Bids are being pulled below market price or moving lower")]
		FadeTheRallyI3 = 105,
		[StringValue("Cluster of buy imbalances near move highs (extreme market buying)")]
		FadeTheRallyFP1 = 106,
		[StringValue("Large buy imbalance near top of price extreme (stuck buyers)")]
		FadeTheRallyFP2 = 107,
		[StringValue("Recent sell imbalances showing up above current market level")]
		FadeTheRallyFP3 = 108,
		[StringValue("Points of control are clustered near extreme highs (ideally above market price)")]
		FadeTheRallyFP4 = 109,
		[StringValue("Equity markets sideways after big drop lower")]
		FadeTheDropMC1 = 110,
		[StringValue("Nasdaq is lower than ES and moving sideways, or higher")]
		FadeTheDropMC2 = 111,
		[StringValue("Risk off sideways near highs or starting to move lower")]
		FadeTheDropMC3 = 112,
		[StringValue("Bids are moving higher and closer to market price")]
		FadeTheDropI1 = 113,
		[StringValue("Offers have stopped moving lower with the trend")]
		FadeTheDropI2 = 114,
		[StringValue("Offers are being pulled above market price or moving higher")]
		FadeTheDropI3 = 115,
		[StringValue("Cluster of sell imbalances near move lows (extreme market selling)")]
		FadeTheDropFP1 = 116,
		[StringValue("Large sell imbalance near low of price extreme (stuck shorts)")]
		FadeTheDropFP2 = 117,
		[StringValue("Recent buy imbalances showing up below current market level")]
		FadeTheDropFP3 = 118,
		[StringValue("Points of control are clustered near extreme lows (ideally below market price)")]
		FadeTheDropFP4 = 119,
		[StringValue("Equity markets up")]
		BuyTheBreakoutMC1 = 120,
		[StringValue("Nasdaq is higher (already broken) and moving faster than ES")]
		BuyTheBreakoutMC2 = 121,
		[StringValue("Risk off down")]
		BuyTheBreakoutMC3 = 122,
		[StringValue("Bids moving higher with trend on heat map")]
		BuyTheBreakoutI1 = 123,
		[StringValue("Offers are moving higher or being canceled (pulled)")]
		BuyTheBreakoutI2 = 124,
		[StringValue("Large offer sitting above current price as potential target")]
		BuyTheBreakoutI3 = 125,
		[StringValue("Buy imbalances are moving higher (market buying at higher prices)")]
		BuyTheBreakoutFP1 = 126,
		[StringValue("Most recent sell imbalance is below the current market price")]
		BuyTheBreakoutFP2 = 127,
		[StringValue("Large sell imbalance trade stuck near highs of current move")]
		BuyTheBreakoutFP3 = 128,
		[StringValue("Points of control are below market price supporting move higher")]
		BuyTheBreakoutFP4 = 129,
		[StringValue("Equity markets down")]
		SellTheBreakoutMC1 = 130,
		[StringValue("Nasdaq is lower (already broken) and moving faster than ES")]
		SellTheBreakoutMC2 = 131,
		[StringValue("Risk off up")]
		SellTheBreakoutMC3 = 132,
		[StringValue("Offers moving lower with trend on heat map")]
		SellTheBreakoutI1 = 133,
		[StringValue("Bids are moving lower or being canceled (pulled)")]
		SellTheBreakoutI2 = 134,
		[StringValue("Large bid sitting below current price as potential target")]
		SellTheBreakoutI3 = 135,
		[StringValue("Sell imbalances are moving lower (market selling at lower prices)")]
		SellTheBreakoutFP1 = 136,
		[StringValue("Most recent buy imbalance is above the current market price")]
		SellTheBreakoutFP2 = 137,
		[StringValue("Large buy imbalance stuck near lows of current move")]
		SellTheBreakoutFP3 = 138,
		[StringValue("Points of control are above market price supporting move lower")]
		SellTheBreakoutFP4 = 139,
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
				case "Equity markets sideways after big rally higher":
					retVal = TradeQualifiers.FadeTheRallyMC1;
					break;
				case "Nasdaq is higher than ES and moving sideways, or lower":
					retVal = TradeQualifiers.FadeTheRallyMC2;
					break;
				case "Risk off sideways near lows or starting to move up":
					retVal = TradeQualifiers.FadeTheRallyMC3;
					break;
				case "Offers are moving lower and closer to market price":
					retVal = TradeQualifiers.FadeTheRallyI1;
					break;
				case "Bids have stopped moving higher with the trend":
					retVal = TradeQualifiers.FadeTheRallyI2;
					break;
				case "Bids are being pulled below market price or moving lower":
					retVal = TradeQualifiers.FadeTheRallyI3;
					break;
				case "Cluster of buy imbalances near move highs (extreme market buying)":
					retVal = TradeQualifiers.FadeTheRallyFP1;
					break;
				case "Large buy imbalance near top of price extreme (stuck buyers)":
					retVal = TradeQualifiers.FadeTheRallyFP2;
					break;
				case "Recent sell imbalances showing up above current market level":
					retVal = TradeQualifiers.FadeTheRallyFP3;
					break;
				case "Points of control are clustered near extreme highs (ideally above market price)":
					retVal = TradeQualifiers.FadeTheRallyFP4;
					break;
				case "Equity markets sideways after big drop lower":
					retVal = TradeQualifiers.FadeTheDropMC1;
					break;
				case "Nasdaq is lower than ES and moving sideways, or higher":
					retVal = TradeQualifiers.FadeTheDropMC2;
					break;
				case "Risk off sideways near highs or starting to move lower":
					retVal = TradeQualifiers.FadeTheDropMC3;
					break;
				case "Bids are moving higher and closer to market price":
					retVal = TradeQualifiers.FadeTheDropI1;
					break;
				case "Offers have stopped moving lower with the trend":
					retVal = TradeQualifiers.FadeTheDropI2;
					break;
				case "Offers are being pulled above market price or moving higher":
					retVal = TradeQualifiers.FadeTheDropI3;
					break;
				case "Cluster of sell imbalances near move lows (extreme market selling)":
					retVal = TradeQualifiers.FadeTheDropFP1;
					break;
				case "Large sell imbalance near low of price extreme (stuck shorts)":
					retVal = TradeQualifiers.FadeTheDropFP2;
					break;
				case "Recent buy imbalances showing up below current market level":
					retVal = TradeQualifiers.FadeTheDropFP3;
					break;
				case "Points of control are clustered near extreme lows (ideally below market price)":
					retVal = TradeQualifiers.FadeTheDropFP4;
					break;
				case "Equity markets up":
					retVal = TradeQualifiers.BuyTheBreakoutMC1;
					break;
				case "Nasdaq is higher (already broken) and moving faster than ES":
					retVal = TradeQualifiers.BuyTheBreakoutMC2;
					break;
				case "Risk off down":
					retVal = TradeQualifiers.BuyTheBreakoutMC3;
					break;
				case "Bids moving higher with trend on heat map":
					retVal = TradeQualifiers.BuyTheBreakoutI1;
					break;
				case "Offers are moving higher or being canceled (pulled)":
					retVal = TradeQualifiers.BuyTheBreakoutI2;
					break;
				case "Large offer sitting above current price as potential target":
					retVal = TradeQualifiers.BuyTheBreakoutI3;
					break;
				case "Buy imbalances are moving higher (market buying at higher prices)":
					retVal = TradeQualifiers.BuyTheBreakoutFP1;
					break;
				case "Most recent sell imbalance is below the current market price":
					retVal = TradeQualifiers.BuyTheBreakoutFP2;
					break;
				case "Large sell imbalance trade stuck near highs of current move":
					retVal = TradeQualifiers.BuyTheBreakoutFP3;
					break;
				case "Points of control are below market price supporting move higher":
					retVal = TradeQualifiers.BuyTheBreakoutFP4;
					break;
				case "Equity markets down":
					retVal = TradeQualifiers.SellTheBreakoutMC1;
					break;
				case "Nasdaq is lower (already broken) and moving faster than ES":
					retVal = TradeQualifiers.SellTheBreakoutMC2;
					break;
				case "Risk off up":
					retVal = TradeQualifiers.SellTheBreakoutMC3;
					break;
				case "Offers moving lower with trend on heat map":
					retVal = TradeQualifiers.SellTheBreakoutI1;
					break;
				case "Bids are moving lower or being canceled (pulled)":
					retVal = TradeQualifiers.SellTheBreakoutI2;
					break;
				case "Large bid sitting below current price as potential target":
					retVal = TradeQualifiers.SellTheBreakoutI3;
					break;
				case "Sell imbalances are moving lower (market selling at lower prices)":
					retVal = TradeQualifiers.SellTheBreakoutFP1;
					break;
				case "Most recent buy imbalance is above the current market price":
					retVal = TradeQualifiers.SellTheBreakoutFP2;
					break;
				case "Large buy imbalance stuck near lows of current move":
					retVal = TradeQualifiers.SellTheBreakoutFP3;
					break;
				case "Points of control are above market price supporting move lower":
					retVal = TradeQualifiers.SellTheBreakoutFP4;
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
			selectItems.Add(new SelectListItem() { Text = "Equity markets sideways after big rally higher", Value = TradeQualifiers.FadeTheRallyMC1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Nasdaq is higher than ES and moving sideways, or lower", Value = TradeQualifiers.FadeTheRallyMC2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Risk off sideways near lows or starting to move up", Value = TradeQualifiers.FadeTheRallyMC3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Offers are moving lower and closer to market price", Value = TradeQualifiers.FadeTheRallyI1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Bids have stopped moving higher with the trend", Value = TradeQualifiers.FadeTheRallyI2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Bids are being pulled below market price or moving lower", Value = TradeQualifiers.FadeTheRallyI3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Cluster of buy imbalances near move highs (extreme market buying)", Value = TradeQualifiers.FadeTheRallyFP1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Large buy imbalance near top of price extreme (stuck buyers)", Value = TradeQualifiers.FadeTheRallyFP2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Recent sell imbalances showing up above current market level", Value = TradeQualifiers.FadeTheRallyFP3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Points of control are clustered near extreme highs (ideally above market price)", Value = TradeQualifiers.FadeTheRallyFP4.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Equity markets sideways after big drop lower", Value = TradeQualifiers.FadeTheDropMC1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Nasdaq is lower than ES and moving sideways, or higher", Value = TradeQualifiers.FadeTheDropMC2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Risk off sideways near highs or starting to move lower", Value = TradeQualifiers.FadeTheDropMC3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Bids are moving higher and closer to market price", Value = TradeQualifiers.FadeTheDropI1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Offers have stopped moving lower with the trend", Value = TradeQualifiers.FadeTheDropI2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Offers are being pulled above market price or moving higher", Value = TradeQualifiers.FadeTheDropI3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Cluster of sell imbalances near move lows (extreme market selling)", Value = TradeQualifiers.FadeTheDropFP1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Large sell imbalance near low of price extreme (stuck shorts)", Value = TradeQualifiers.FadeTheDropFP2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Recent buy imbalances showing up below current market level", Value = TradeQualifiers.FadeTheDropFP3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Points of control are clustered near extreme lows (ideally below market price)", Value = TradeQualifiers.FadeTheDropFP4.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Equity markets up", Value = TradeQualifiers.BuyTheBreakoutMC1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Nasdaq is higher (already broken) and moving faster than ES", Value = TradeQualifiers.BuyTheBreakoutMC2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Risk off down", Value = TradeQualifiers.BuyTheBreakoutMC3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Bids moving higher with trend on heat map", Value = TradeQualifiers.BuyTheBreakoutI1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Offers are moving higher or being canceled (pulled)", Value = TradeQualifiers.BuyTheBreakoutI2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Large offer sitting above current price as potential target", Value = TradeQualifiers.BuyTheBreakoutI3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Buy imbalances are moving higher (market buying at higher prices)", Value = TradeQualifiers.BuyTheBreakoutFP1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Most recent sell imbalance is below the current market price", Value = TradeQualifiers.BuyTheBreakoutFP2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Large sell imbalance trade stuck near highs of current move", Value = TradeQualifiers.BuyTheBreakoutFP3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Points of control are below market price supporting move higher", Value = TradeQualifiers.BuyTheBreakoutFP4.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Equity markets down", Value = TradeQualifiers.SellTheBreakoutMC1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Nasdaq is lower (already broken) and moving faster than ES", Value = TradeQualifiers.SellTheBreakoutMC2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Risk off up", Value = TradeQualifiers.SellTheBreakoutMC3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Offers moving lower with trend on heat map", Value = TradeQualifiers.SellTheBreakoutI1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Bids are moving lower or being canceled (pulled)", Value = TradeQualifiers.SellTheBreakoutI2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Large bid sitting below current price as potential target", Value = TradeQualifiers.SellTheBreakoutI3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Sell imbalances are moving lower (market selling at lower prices)", Value = TradeQualifiers.SellTheBreakoutFP1.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Most recent buy imbalance is above the current market price", Value = TradeQualifiers.SellTheBreakoutFP2.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Large buy imbalance stuck near lows of current move", Value = TradeQualifiers.SellTheBreakoutFP3.ToString() });
			selectItems.Add(new SelectListItem() { Text = "Points of control are above market price supporting move lower", Value = TradeQualifiers.SellTheBreakoutFP4.ToString() });
			return selectItems;
		}
	}
}
