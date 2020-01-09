using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeProAssistant.Models
{
    public class SecurityModel : SecurityDto
    {
        #region Properties
        public String SymbolAndName
        {
            get
            {
                return String.Format("{0} - {1}", this.Symbol, this.Name);
            }
        }
        #endregion
    }
}