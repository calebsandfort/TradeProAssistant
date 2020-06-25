using Entities.Dtos;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Models
{
    public class TradeTicketModel : TradeTicketDto
    {
        #region MarketStructureQualifiersForStrategy
        private List<MarketStructureQualifiers> _marketStructureQualifiersForStrategy = null;

        public List<MarketStructureQualifiers> MarketStructureQualifiersForStrategy
        {
            get
            {
                if (_marketStructureQualifiersForStrategy == null)
                {
                    _marketStructureQualifiersForStrategy = new List<MarketStructureQualifiers>();

                    switch (this.Strategy)
                    {
                        case Strategies.None:
                            break;
                        case Strategies.BuyTheDip:
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.HigherHighOnImpulse);
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.HigherLowsOnImpulse);
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.PreviousHighIsSupport);
                            break;
                        case Strategies.SellTheRip:
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.LowerLowOnImpulse);
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.LowerHighsOnImpulse);
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.PreviousLowIsResistance);
                            break;
                        case Strategies.FadeTheRally:
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.FadeTheRally1);
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.FadeTheRally2);
                            break;
                        case Strategies.FadeTheDrop:
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.FadeTheDrop1);
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.FadeTheDrop2);
                            break;
                        case Strategies.BuyTheBreakout:
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.BuyTheBreakout1);
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.BuyTheBreakout2);
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.BuyTheBreakout3);
                            break;
                        case Strategies.SellTheBreakout:
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.SellTheBreakout1);
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.SellTheBreakout2);
                            _marketStructureQualifiersForStrategy.Add(Enums.MarketStructureQualifiers.SellTheBreakout3);
                            break;
                        default:
                            break;
                    }
                }

                return _marketStructureQualifiersForStrategy;
            }
        }
        #endregion

        #region TradeQualifiersForStrategy
        private List<TradeQualifiersModel> _tradeQualifiers = null;

        public List<TradeQualifiersModel> TradeQualifiersForStrategy
        {
            get
            {
                if (_tradeQualifiers == null)
                {
                    _tradeQualifiers = new List<TradeQualifiersModel>();

                    switch (this.Strategy)
                    {
                        case Enums.Strategies.None:
                            break;
                        #region BuyTheDip
                        case Enums.Strategies.BuyTheDip:
                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.Inventory,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.BidsMovingHigher,
                                    TradeQualifiers.OffersMovingHigherOrPulled,
                                    TradeQualifiers.OfferMagnet
                                }
                            });

                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.FootprintCharts,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.BuyImbalancesMovingHigher,
                                    TradeQualifiers.MostRecentSellImbalanceBelowCurrentPrice,
                                    TradeQualifiers.LargeSellImbalanceNearLowsOfPriorMove,
                                    TradeQualifiers.PointsOfControlBelowMarketPrice
                                }
                            });
                            break;
                        #endregion
                        #region SellTheRip
                        case Enums.Strategies.SellTheRip:
                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.Inventory,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.OffersMovingLower,
                                    TradeQualifiers.BidsMovingLowerOrPulled,
                                    TradeQualifiers.BidMagnet
                                }
                            });

                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.FootprintCharts,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.SellImbalancesMovingLower,
                                    TradeQualifiers.MostRecentBuyImbalanceAboveCurrentPrice,
                                    TradeQualifiers.LargeBuyImbalanceNearHighsOfPriorMove,
                                    TradeQualifiers.PointsOfControlAboveMarketPrice
                                }
                            });
                            break;
                        #endregion
                        #region FadeTheRally
                        case Enums.Strategies.FadeTheRally:
                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.Inventory,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.FadeTheRallyI1,
                                    TradeQualifiers.FadeTheRallyI2,
                                    TradeQualifiers.FadeTheRallyI3
                                }
                            });

                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.FootprintCharts,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.FadeTheRallyFP1,
                                    TradeQualifiers.FadeTheRallyFP2,
                                    TradeQualifiers.FadeTheRallyFP3,
                                    TradeQualifiers.FadeTheRallyFP4
                                }
                            });
                            break;
                        #endregion
                        #region FadeTheDrop
                        case Enums.Strategies.FadeTheDrop:
                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.Inventory,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.FadeTheDropI1,
                                    TradeQualifiers.FadeTheDropI2,
                                    TradeQualifiers.FadeTheDropI3
                                }
                            });

                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.FootprintCharts,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.FadeTheDropFP1,
                                    TradeQualifiers.FadeTheDropFP2,
                                    TradeQualifiers.FadeTheDropFP3,
                                    TradeQualifiers.FadeTheDropFP4
                                }
                            });
                            break;
                        #endregion
                        #region BuyTheBreakout
                        case Enums.Strategies.BuyTheBreakout:
                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.Inventory,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.BuyTheBreakoutI1,
                                    TradeQualifiers.BuyTheBreakoutI2,
                                    TradeQualifiers.BuyTheBreakoutI3
                                }
                            });

                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.FootprintCharts,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.BuyTheBreakoutFP1,
                                    TradeQualifiers.BuyTheBreakoutFP2,
                                    TradeQualifiers.BuyTheBreakoutFP3,
                                    TradeQualifiers.BuyTheBreakoutFP4
                                }
                            });
                            break;
                        #endregion
                        #region SellTheBreakout
                        case Enums.Strategies.SellTheBreakout:
                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.Inventory,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.SellTheBreakoutI1,
                                    TradeQualifiers.SellTheBreakoutI2,
                                    TradeQualifiers.SellTheBreakoutI3
                                }
                            });

                            _tradeQualifiers.Add(new TradeQualifiersModel()
                            {
                                TradeQualifierType = TradeQualifierTypes.FootprintCharts,
                                TradeQualifiersList = new List<TradeQualifiers>()
                                {
                                    TradeQualifiers.SellTheBreakoutFP1,
                                    TradeQualifiers.SellTheBreakoutFP2,
                                    TradeQualifiers.SellTheBreakoutFP3,
                                    TradeQualifiers.SellTheBreakoutFP4
                                }
                            });
                            break;
                        #endregion
                        default:
                            break;
                    }
                }

                return _tradeQualifiers;
            }
        }
        #endregion
    }
}