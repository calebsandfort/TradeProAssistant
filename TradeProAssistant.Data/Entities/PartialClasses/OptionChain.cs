using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using TradeProAssistant.Data.Models;

namespace Entities
{
	public partial class OptionChain
	{
        #region Custom Properties

        #endregion

        #region Custom Constructors
        public OptionChain(OpcGetOptionChainResponse opcGetOptionChainResponse) : this()
        {
            this.Date = DateTime.Now;

            if(opcGetOptionChainResponse.Dates != null && opcGetOptionChainResponse.Dates.Count > 0)
            {
                int dateIndex = this.Date.DayOfWeek == DayOfWeek.Friday ? 1 : 0;
                KeyValuePair<String, OpcDate> opcDatePair = opcGetOptionChainResponse.Dates.ElementAt(dateIndex);

                OptionDate optionDate = new OptionDate();
                optionDate.ExpiryDate = DateTime.Parse(opcDatePair.Key);

                foreach (KeyValuePair<String, OpcOptionPriceSpread> callPair in opcDatePair.Value.Calls)
                {
                    OptionStrike optionStrike = new OptionStrike();
                    optionStrike.StrikePrice = Decimal.Parse(callPair.Key);

                    Call call = new Call();
                    call.Bid = callPair.Value.Bid;
                    call.Ask = callPair.Value.Ask;
                    optionStrike.Call = call;

                    OpcOptionPriceSpread putPair = opcDatePair.Value.Puts[callPair.Key];
                    Put put = new Put();
                    put.Bid = putPair.Bid;
                    put.Ask = putPair.Ask;
                    optionStrike.Put = put;

                    optionDate.Strikes.Add(optionStrike);
                }

                this.Dates.Add(optionDate);
            }
        }
        #endregion

        #region Custom Methods

        #endregion
    }
}
