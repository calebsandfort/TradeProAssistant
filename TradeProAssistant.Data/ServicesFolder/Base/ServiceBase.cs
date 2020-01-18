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
        #region ProgressMessage
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

        #region Redirect
        protected virtual void OnRedirectRaised(String controller, String action, int id, String jobId)
        {
            EventHandler<RedirectEventArgs> handler = RedirectRaised;
            if (handler != null)
            {
                handler(this, new RedirectEventArgs()
                {
                    Controller = controller,
                    Action = action,
                    Id = id,
                    JobId = jobId
                });
            }
        }

        public event EventHandler<RedirectEventArgs> RedirectRaised;
        #endregion
        #endregion
    }
}
