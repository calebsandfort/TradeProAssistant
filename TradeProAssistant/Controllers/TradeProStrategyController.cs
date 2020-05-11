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
                Quantity = 1
            });
        }

        public ActionResult PullbackTradeTicketForm()
        {
            return View(new PullbackTradeTicketDto()
            {
                Timestamp = DateTime.Now,
                Asset = Enums.TradeProAssets.ES,
                Quantity = 1
            });
        }

        [HttpPost]
        public JsonResult PullbackTradeTicketForm(PullbackTradeTicketDto dto)
        {
            dto.Identifier = PullbackTradeTicketService.Save(mapper.Map<PullbackTradeTicket>(dto));
            return Json(dto);
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