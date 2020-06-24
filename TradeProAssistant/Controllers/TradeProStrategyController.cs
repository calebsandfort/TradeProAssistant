using AutoMapper;
using Data.Framework;
using Entities;
using Entities.Dtos;
using Kendo.Mvc.UI;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeProAssistant.Models;
using TradeProAssistant.Framework;
using TradeProAssistant.Data.Models;
using Enums;
using TradeProAssistant.Utilities;

namespace TradeProAssistant.Controllers
{
    public class TradeProStrategyController : Controller
    {
        #region Ctor
        private readonly IMapper mapper;

        public TradeProStrategyController()
        {
        }

        public TradeProStrategyController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        #endregion

        public ActionResult Trading()
        {
            return View(new TradeTicketDto()
            {
                Timestamp = DateTime.Now,
                Asset = Enums.TradeProAssets.ES,
                Strategy = Enums.Strategies.BuyTheDip,
                Quantity = 1
            });
        }

        public ActionResult TradeTicketForm()
        {
            return View(new TradeTicketDto()
            {
                Timestamp = DateTime.Now,
                Asset = Enums.TradeProAssets.ES,
                Strategy = Enums.Strategies.BuyTheDip,
                Quantity = 1
            });
        }

        public ActionResult Visuals()
        {
            return PartialView("_Visuals", new TradeTicketDto()
            {
                Strategy = Enums.Strategies.BuyTheDip,
            });
        }

        [HttpPost]
        public ActionResult Visuals(Strategies strategy)
        {
            return PartialView("_Visuals", new TradeTicketDto()
            {
                Strategy = strategy,
            });
        }

        [HttpPost]
        public JsonResult TradeTicketForm(TradeTicketDto dto)
        {
            dto.Identifier = TradeTicketService.Save(mapper.Map<TradeTicket>(dto));
            return Json(dto);
        }

        public ActionResult MarketStructureQualifiers(Strategies strategy)
        {
            MarketStructureQualifiersModel model = new MarketStructureQualifiersModel();

            switch (strategy)
            {
                case Strategies.None:
                    break;
                case Strategies.BuyTheDip:
                    model.MarketStructureQualifier1 = Enums.MarketStructureQualifiers.HigherHighOnImpulse;
                    model.MarketStructureQualifier2 = Enums.MarketStructureQualifiers.HigherLowsOnImpulse;
                    model.MarketStructureQualifier3 = Enums.MarketStructureQualifiers.PreviousHighIsSupport;
                    break;
                case Strategies.SellTheRip:
                    model.MarketStructureQualifier1 = Enums.MarketStructureQualifiers.LowerLowOnImpulse;
                    model.MarketStructureQualifier2 = Enums.MarketStructureQualifiers.LowerHighsOnImpulse;
                    model.MarketStructureQualifier3 = Enums.MarketStructureQualifiers.PreviousLowIsResistance;
                    break;
                case Strategies.FadeTheRally:
                    model.MarketStructureQualifier1 = Enums.MarketStructureQualifiers.FadeTheRally1;
                    model.MarketStructureQualifier2 = Enums.MarketStructureQualifiers.FadeTheRally2;
                    model.MarketStructureQualifier3 = Enums.MarketStructureQualifiers.None;
                    break;
                case Strategies.FadeTheDrop:
                    model.MarketStructureQualifier1 = Enums.MarketStructureQualifiers.FadeTheDrop1;
                    model.MarketStructureQualifier2 = Enums.MarketStructureQualifiers.FadeTheDrop2;
                    model.MarketStructureQualifier3 = Enums.MarketStructureQualifiers.None;
                    break;
                case Strategies.BuyTheBreakout:
                    model.MarketStructureQualifier1 = Enums.MarketStructureQualifiers.BuyTheBreakout1;
                    model.MarketStructureQualifier2 = Enums.MarketStructureQualifiers.BuyTheBreakout2;
                    model.MarketStructureQualifier3 = Enums.MarketStructureQualifiers.BuyTheBreakout3;
                    break;
                case Strategies.SellTheBreakout:
                    model.MarketStructureQualifier1 = Enums.MarketStructureQualifiers.SellTheBreakout1;
                    model.MarketStructureQualifier2 = Enums.MarketStructureQualifiers.SellTheBreakout2;
                    model.MarketStructureQualifier3 = Enums.MarketStructureQualifiers.SellTheBreakout3;
                    break;
                default:
                    break;
            }

            return PartialView("_MarketStructureQualifiers", model);
        }

        public ActionResult TradeTicketQualifiers(Strategies strategy)
        {
            List<TradeQualifiersModel> model = new List<TradeQualifiersModel>();

            switch (strategy)
            {
                case Enums.Strategies.None:
                    break;
                #region BuyTheDip
                case Enums.Strategies.BuyTheDip:
                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.MarketCorrelations,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.EquityMarketsUp,
                            TradeQualifiers.NasdaqHigherFaster,
                            TradeQualifiers.RiskOffDown
                        }
                    });

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Inventory,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.BidsMovingHigher,
                            TradeQualifiers.OffersMovingHigherOrPulled,
                            TradeQualifiers.OfferMagnet
                        }
                    });

                    model.Add(new TradeQualifiersModel()
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

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Misc,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.Volume,
                            TradeQualifiers.PivotPoints,
                            TradeQualifiers.Technicals,
                            TradeQualifiers.MarketStructure
                        }
                    });
                    break;
                #endregion
                #region SellTheRip
                case Enums.Strategies.SellTheRip:
                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.MarketCorrelations,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.EquityMarketsDown,
                            TradeQualifiers.NasdaqLowerFaster,
                            TradeQualifiers.RiskOffUp
                        }
                    });

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Inventory,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.OffersMovingLower,
                            TradeQualifiers.BidsMovingLowerOrPulled,
                            TradeQualifiers.BidMagnet
                        }
                    });

                    model.Add(new TradeQualifiersModel()
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

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Misc,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.Volume,
                            TradeQualifiers.PivotPoints,
                            TradeQualifiers.Technicals,
                            TradeQualifiers.MarketStructure
                        }
                    });
                    break;
                #endregion
                #region FadeTheRally
                case Enums.Strategies.FadeTheRally:
                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.MarketCorrelations,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.FadeTheRallyMC1,
                            TradeQualifiers.FadeTheRallyMC2,
                            TradeQualifiers.FadeTheRallyMC3
                        }
                    });

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Inventory,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.FadeTheRallyI1,
                            TradeQualifiers.FadeTheRallyI2,
                            TradeQualifiers.FadeTheRallyI3
                        }
                    });

                    model.Add(new TradeQualifiersModel()
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

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Misc,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.Volume,
                            TradeQualifiers.PivotPoints,
                            TradeQualifiers.Technicals,
                            TradeQualifiers.MarketStructure
                        }
                    });
                    break;
                #endregion
                #region FadeTheDrop
                case Enums.Strategies.FadeTheDrop:
                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.MarketCorrelations,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.FadeTheDropMC1,
                            TradeQualifiers.FadeTheDropMC2,
                            TradeQualifiers.FadeTheDropMC3
                        }
                    });

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Inventory,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.FadeTheDropI1,
                            TradeQualifiers.FadeTheDropI2,
                            TradeQualifiers.FadeTheDropI3
                        }
                    });

                    model.Add(new TradeQualifiersModel()
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

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Misc,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.Volume,
                            TradeQualifiers.PivotPoints,
                            TradeQualifiers.Technicals,
                            TradeQualifiers.MarketStructure
                        }
                    });
                    break;
                #endregion
                #region BuyTheBreakout
                case Enums.Strategies.BuyTheBreakout:
                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.MarketCorrelations,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.BuyTheBreakoutMC1,
                            TradeQualifiers.BuyTheBreakoutMC2,
                            TradeQualifiers.BuyTheBreakoutMC3
                        }
                    });

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Inventory,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.BuyTheBreakoutI1,
                            TradeQualifiers.BuyTheBreakoutI2,
                            TradeQualifiers.BuyTheBreakoutI3
                        }
                    });

                    model.Add(new TradeQualifiersModel()
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

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Misc,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.Volume,
                            TradeQualifiers.PivotPoints,
                            TradeQualifiers.Technicals,
                            TradeQualifiers.MarketStructure
                        }
                    });
                    break;
                #endregion
                #region SellTheBreakout
                case Enums.Strategies.SellTheBreakout:
                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.MarketCorrelations,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.SellTheBreakoutMC1,
                            TradeQualifiers.SellTheBreakoutMC2,
                            TradeQualifiers.SellTheBreakoutMC3
                        }
                    });

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Inventory,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.SellTheBreakoutI1,
                            TradeQualifiers.SellTheBreakoutI2,
                            TradeQualifiers.SellTheBreakoutI3
                        }
                    });

                    model.Add(new TradeQualifiersModel()
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

                    model.Add(new TradeQualifiersModel()
                    {
                        TradeQualifierType = TradeQualifierTypes.Misc,
                        TradeQualifiersList = new List<TradeQualifiers>()
                        {
                            TradeQualifiers.Volume,
                            TradeQualifiers.PivotPoints,
                            TradeQualifiers.Technicals,
                            TradeQualifiers.MarketStructure
                        }
                    });
                    break; 
                #endregion
                default:
                    break;
            }

            return PartialView("_TradeTicketQualifiers", model);
        }

        #region TradeTickets_Read
        public ActionResult TradeTickets_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = new DataSourceResult();
            Query query = request.ToQuery();

            List<TradeTicketDto> tradeTickets = mapper.Map<List<TradeTicketDto>>(TradeTicketService.GetCollection(query));

            result.Data = tradeTickets;
            result.Total = tradeTickets.Count;

            return new GuerillaLogisticsApiJsonResult(result);
        }
        #endregion

        #region DayPerformance_Read
        public ActionResult DayPerformance_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<DayPerformanceModel> list = TradeTicketService.GetDayPerformance();

            DataSourceResult result = new DataSourceResult();
            result.Data = list;
            result.Total = list.Count;

            return new GuerillaLogisticsApiJsonResult(result);
        }
        #endregion

        #region TradePerformance
        public ActionResult TradePerformance()
        {
            return PartialView("_TradePerformance", TradeTicketService.GetMonthPerformance(GlobalSettings.ActiveTradingSettings.MonthStart));
        }
        #endregion
    }
}