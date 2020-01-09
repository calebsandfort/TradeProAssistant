using AutoMapper;
using Contexts;
using Data.Framework;
using Entities;
using Entities.Dtos;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeProAssistant.Framework;
using TradeProAssistant.Models;

namespace TradeProAssistant.Controllers
{
    public class ScannerController : Controller
    {
        private readonly IMapper mapper;

        public ScannerController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        // GET: Scanner
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSecurities(String text)
        {
            using (TradeProAssistantContext context = new TradeProAssistantContext())
            {
                List<SecurityModel> result = mapper.Map<List<SecurityModel>>(context.Securities
                    .Where(x => x.Symbol.Contains(text) || x.Name.Contains(text))
                    .OrderBy(x => x.Symbol).ToList());

                return new GuerillaLogisticsApiJsonResult(result);
            }
        }
    }
}