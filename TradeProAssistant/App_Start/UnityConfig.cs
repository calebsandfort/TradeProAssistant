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

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Security, SecurityDto>();
                cfg.CreateMap<Security, SecurityModel>();
                cfg.CreateMap<DayCandlestick, DayCandlestickDto>();
                cfg.CreateMap<WeekCandlestick, WeekCandlestickDto>();
            });
            IMapper mapper = config.CreateMapper();
            container.RegisterInstance(mapper);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}