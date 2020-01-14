﻿using Data.Framework;
using Entities;
using HtmlBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradeProAssistant.Data.Framework;
using Services;
using TradeProAssistant.Data.Framework;

namespace Services
{
    public class WeeklyIncomeService : ServiceBase, IDisposable
    {
        #region Properties
        public String JobId { get; set; }
        #endregion

        #region Constructors
        public WeeklyIncomeService()
        {

        }

        public WeeklyIncomeService(String jobId)
        {
            this.JobId = jobId;
        }
        #endregion

        #region BuildPlan
        public async Task BuildPlan()
        {
            Thread.Sleep(1000);

            DateTime monday = DateTime.Now.GetPreviousDayOfWeekOccurrence(DayOfWeek.Monday);
            DateTime friday = DateTime.Now.GetPreviousDayOfWeekOccurrence(DayOfWeek.Friday);

            Query query = new Query();
            query.SortPropertyName = Security.PropertyNames.Symbol;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.PairEligible,
                Parameter = "true",
                QueryOperator = QueryOperators.Equals
            });

            query.Includes.Add(Security.PropertyNames.OptionChainsInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.DatesInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.Dates.StrikesInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.Dates.Strikes.CallInclude);
            query.Includes.Add(Security.PropertyNames.OptionChains.Dates.Strikes.PutInclude);

            List<Security> securities = SecurityService.GetCollection(query);

            securities.RemoveAll(x => (x.NextEarningsDate.HasValue && x.NextEarningsDate >= monday && x.NextEarningsDate <= friday));
            securities.RemoveAll(x => (x.ExDividendDate.HasValue && x.ExDividendDate >= monday && x.ExDividendDate <= friday));

            OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item").Class("active")
                        .Append(HtmlTags.B.Append(securities.Count.ToString()))
                        .Append(HtmlTags.Span.Append("&nbsp;optionable securities retrieved"))
                        .ToHtml().ToString(), this.JobId);
        }
        #endregion

        #region DownloadOptionChains
        public async Task DownloadOptionChains()
        {
            Thread.Sleep(1000);

            DateTime monday = DateTime.Now.GetPreviousDayOfWeekOccurrence(DayOfWeek.Monday);
            DateTime friday = DateTime.Now.GetPreviousDayOfWeekOccurrence(DayOfWeek.Friday);

            Query query = new Query();
            query.SortPropertyName = Security.PropertyNames.Symbol;
            query.QuerySingleFilters.Add(new QuerySingleFilter()
            {
                PropertyName = Security.PropertyNames.PairEligible,
                Parameter = "true",
                QueryOperator = QueryOperators.Equals
            });

            List<Security> securities = SecurityService.GetCollection(query);

            securities.RemoveAll(x => (x.NextEarningsDate.HasValue && x.NextEarningsDate >= monday && x.NextEarningsDate <= friday));
            securities.RemoveAll(x => (x.ExDividendDate.HasValue && x.ExDividendDate >= monday && x.ExDividendDate <= friday));

            for (int i = 0; i < securities.Count; i++)
            {
                Security security = securities[i];

                OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item").Class("active")
                        .Append(HtmlTags.Span.Append("Processing security:&nbsp;"))
                        .Append(HtmlTags.B.Append(security.Symbol)).ToHtml().ToString(), this.JobId);

                if (SecurityService.GetCurrentPrice(security))
                {
                    SecurityService.Save(security);

                    OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
                        .Append(HtmlTags.B.Append(security.Symbol))
                        .Append(HtmlTags.Span.Append($"&nbsp;current price: {security.CurrentPrice:C}"))
                        .ToHtml().ToString(), this.JobId);

                    OptionChain optionChain = OptionChainService.GetOpcOptionChain(security);
                    if (optionChain != null)
                    {
                        OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
                        .Append(HtmlTags.B.Append(security.Symbol))
                        .Append(HtmlTags.Span.Append("&nbsp;option chain retrieved"))
                        .ToHtml().ToString(), this.JobId);

                        //Decimal expectedMove = 0.0m;
                        //if(optionChain.Dates[0].Strikes.Any(x => x.StrikePrice == security.CurrentPrice))
                        //{
                        //    OptionStrike optionStrike = optionChain.Dates[0].Strikes.First(x => x.StrikePrice == security.CurrentPrice);
                        //    expectedMove = (optionStrike.Call.Ask + optionStrike.Put.Ask) / 2;
                        //}
                        //else
                        //{
                        //    for(int j = 0; j < optionChain.Dates[0].Strikes.Count - 1; j++)
                        //    {
                        //        OptionStrike lowStrike = optionChain.Dates[0].Strikes[j];
                        //        OptionStrike highStrike = optionChain.Dates[0].Strikes[j + 1];

                        //        if(security.CurrentPrice > lowStrike.StrikePrice && security.CurrentPrice < highStrike.StrikePrice)
                        //        {
                        //            expectedMove = (lowStrike.Call.Ask + lowStrike.Put.Ask + highStrike.Call.Ask + highStrike.Put.Ask) / 4;
                        //        }
                        //    }
                        //}

                        //OnProgressMessageRaised(new HtmlTag("li").Class("list-group-item")
                        //.Append(HtmlTags.B.Append(security.Symbol))
                        //.Append(HtmlTags.Span.Append("&nbsp;expected move:&nbsp;"))
                        //.Append(HtmlTags.B.Append($"{expectedMove:C} ({(expectedMove / security.CurrentPrice):P2})"))
                        //.ToHtml().ToString(), this.JobId);

                        OptionChainService.Save(optionChain);
                    }
                }
            }
        } 
        #endregion

        public void Dispose()
        {
        }
    }
}
