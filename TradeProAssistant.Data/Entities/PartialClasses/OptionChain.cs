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
                int dateIndex = (this.Date.DayOfWeek == DayOfWeek.Friday || this.Date.DayOfWeek == DayOfWeek.Friday) ? 1 : 0;
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

        #region Comparisons
        public static bool operator ==(OptionChain entity, object obj)
        {
            if ((object)entity == null && obj == null)
            {
                return true;
            }
            else if ((object)entity != null && obj is OptionChain && entity.GetType() == obj.GetType())
            {
                return (entity.Identifier == ((OptionChain)obj).Identifier);
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(OptionChain entity, object obj)
        {
            if ((object)entity == null && obj == null)
            {
                return false;
            }
            else if ((object)entity != null && obj is OptionChain && entity.GetType() == obj.GetType())
            {
                return (entity.Identifier != ((OptionChain)obj).Identifier);
            }
            else
            {
                return true;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is OptionChain && this.GetType() == obj.GetType())
            {
                return (this.Identifier == ((OptionChain)obj).Identifier);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return Date.ToString();
        }
        #endregion
    }
}
