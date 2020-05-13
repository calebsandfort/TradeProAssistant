using AutoMapper;
using Data.Framework;
using Entities;
using Entities.Dtos;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Utilities
{
    public static class GlobalSettings
    {
        #region Properties
        #region ActiveRiskParameters
        private static RiskParametersDto activeRiskParameters = null;

        public static RiskParametersDto ActiveRiskParameters
        {
            get
            {
                if (activeRiskParameters == null)
                {
                    Query query = new Query();
                    query.SortPropertyName = RiskParameters.PropertyNames.Active;
                    query.QuerySingleFilters.Add(new QuerySingleFilter()
                    {
                        PropertyName = RiskParameters.PropertyNames.Active,
                        Parameter = "true",
                        QueryOperator = QueryOperators.Equals,
                        IsAndFilter = true
                    });

                    activeRiskParameters = mapper.Map<RiskParametersDto>(RiskParametersService.Get(query));
                }

                return activeRiskParameters;
            }
            set { activeRiskParameters = value; }
        }
        #endregion

        #region ActiveTradingSettings
        private static TradingSettingsDto activeTradingSettings = null;

        public static TradingSettingsDto ActiveTradingSettings
        {
            get
            {
                if (activeTradingSettings == null)
                {
                    Query query = new Query();
                    query.Includes = new List<string>() { TradingSettings.PropertyNames.RiskParametersInclude };
                    query.SortPropertyName = TradingSettings.PropertyNames.Identifier;
                    query.QuerySingleFilters.Add(new QuerySingleFilter()
                    {
                        PropertyName = TradingSettings.PropertyNames.Identifier,
                        Parameter = "0",
                        QueryOperator = QueryOperators.GreaterThan,
                        IsAndFilter = true
                    });

                    activeTradingSettings = mapper.Map<TradingSettingsDto>(TradingSettingsService.Get(query));
                }

                return activeTradingSettings;
            }
            set { activeTradingSettings = value; }
        }
        #endregion
        #endregion

        #region Ctor
        private static readonly IMapper mapper;

        static GlobalSettings()
        {
            MapperConfiguration config = UnityConfig.GetAutoMapperConfig();
            mapper = config.CreateMapper();
        }
        #endregion
    }
}