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