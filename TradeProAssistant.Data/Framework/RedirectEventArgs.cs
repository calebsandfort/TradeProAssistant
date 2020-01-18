using System;

namespace TradeProAssistant.Data.Framework
{
    public class RedirectEventArgs : EventArgs
    {
        public String Controller { get; set; }
        public String Action { get; set; }
        public int Id { get; set; }
        public String JobId { get; set; }

        public RedirectEventArgs()
        {
            this.Id = 0;
        }
    }
}
