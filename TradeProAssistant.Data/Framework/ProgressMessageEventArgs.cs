using System;

namespace TradeProAssistant.Data.Framework
{
    public class ProgressMessageEventArgs : EventArgs
    {
        public String ProgressMessage { get; set; }
        public String JobId { get; set; }
    }
}
