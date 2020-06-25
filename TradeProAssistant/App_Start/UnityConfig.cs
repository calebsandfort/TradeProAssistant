using AutoMapper;
using Entities;
using Entities.Dtos;
using System.Web.Mvc;
using TradeProAssistant.Models;
using Unity;
using Unity.Mvc5;

namespace TradeProAssistant
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            MapperConfiguration config = GetAutoMapperConfig();
            IMapper mapper = config.CreateMapper();
            container.RegisterInstance(mapper);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static MapperConfiguration GetAutoMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Security, SecurityDto>();
                cfg.CreateMap<Security, SecurityModel>();
                cfg.CreateMap<DayCandlestick, DayCandlestickDto>();
                cfg.CreateMap<WeekCandlestick, WeekCandlestickDto>();
                cfg.CreateMap<WeeklyIncomePlaySheet, WeeklyIncomePlaySheetDto>();
                cfg.CreateMap<WeeklyIncomePlaySheet, SimpleWeeklyIncomePlaySheetDto>();
                cfg.CreateMap<WeeklyIncomeActionPlan, WeeklyIncomeActionPlanDto>();
                cfg.CreateMap<WeeklyIncomeComboCount, WeeklyIncomeComboCountDto>();
                cfg.CreateMap<BullPutSpread, BullPutSpreadDto>();
                cfg.CreateMap<BearCallSpread, BearCallSpreadDto>();
                cfg.CreateMap<Put, PutDto>();
                cfg.CreateMap<Call, CallDto>();
                cfg.CreateMap<PairCondor, PairCondorDto>();
                cfg.CreateMap<TradeTicket, TradeTicketDto>();
                cfg.CreateMap<TradeTicket, TradeTicketModel>();
                cfg.CreateMap<TradeTicketDto, TradeTicket>();
                cfg.CreateMap<RiskParameters, RiskParametersDto>();
                cfg.CreateMap<TradingSettings, TradingSettingsDto>();
            });
        }
    }
}