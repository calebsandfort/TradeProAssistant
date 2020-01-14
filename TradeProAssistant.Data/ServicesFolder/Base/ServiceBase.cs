using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeProAssistant.Data.Framework;

namespace Services
{
    public class ServiceBase
    {
        #region Events
        protected virtual void OnProgressMessageRaised(String progressMessage, String jobId)
        {
            EventHandler<ProgressMessageEventArgs> handler = ProgressMessageRaised;
            if (handler != null)
            {
                handler(this, new ProgressMessageEventArgs() { ProgressMessage = progressMessage, JobId = jobId });
            }
        }

        public event EventHandler<ProgressMessageEventArgs> ProgressMessageRaised;
        #endregion
    }
}
