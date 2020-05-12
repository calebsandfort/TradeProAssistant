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

        public ActionResult Strategies()
        {
            return View(new StrategiesModel
            {
                PullbackTradeTicket = new PullbackTradeTicketDto()
                {
                    Timestamp = DateTime.Now,
                    Asset = Enums.TradeProAssets.ES,
                    Strategy = Enums.Strategies.BuyTheDip,
                    Quantity = 1
                }
            });
        }

        public ActionResult PullbackStrategyTab()
        {
            return View(new PullbackTradeTicketDto()
            {
                Timestamp = DateTime.Now,
                Asset = Enums.TradeProAssets.ES,
                Strategy = Enums.Strategies.BuyTheDip,
                Quantity = 1
            });
        }

        public ActionResult PullbackTradeTicketForm()
        {
            return View(new PullbackTradeTicketDto()
            {
                Timestamp = DateTime.Now,
                Asset = Enums.TradeProAssets.ES,
                Strategy = Enums.Strategies.BuyTheDip,
                Quantity = 1
            });
        }

        [HttpPost]
        public JsonResult PullbackTradeTicketForm(PullbackTradeTicketDto dto)
        {
            dto.Identifier = PullbackTradeTicketService.Save(mapper.Map<PullbackTradeTicket>(dto));
            return Json(dto);
        }

        public ActionResult TradeTicketQualifiers(Strategies strategy)
        {
            List<TradeQualifiersModel> model = new List<TradeQualifiersModel>();

            switch (strategy)
            {
                case Enums.Strategies.None:
                    break;
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
                case Enums.Strategies.FadeTheRally:
                    break;
                case Enums.Strategies.FadeTheDrop:
                    break;
                case Enums.Strategies.BuyTheBreakout:
                    break;
                case Enums.Strategies.SellTheBreakout:
                    break;
                default:
                    break;
            }

            return PartialView("_TradeTicketQualifiers", model);
        }

        #region PullbackTradeTickets_Read
        public ActionResult PullbackTradeTickets_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = new DataSourceResult();
            Query query = request.ToQuery();

            List<PullbackTradeTicketDto> tradeTickets = mapper.Map<List<PullbackTradeTicketDto>>(PullbackTradeTicketService.GetCollection(query));

            result.Data = tradeTickets;
            result.Total = tradeTickets.Count;

            return new GuerillaLogisticsApiJsonResult(result);
        }
        #endregion

        #region PullbackDayPerformance_Read
        public ActionResult PullbackDayPerformance_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<DayPerformanceModel> list = PullbackTradeTicketService.GetDayPerformance();

            DataSourceResult result = new DataSourceResult();
            result.Data = list;
            result.Total = list.Count;

            return new GuerillaLogisticsApiJsonResult(result);
        }
        #endregion
    }
}