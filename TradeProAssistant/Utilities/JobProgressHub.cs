using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TradeProAssistant.Utilities
{
    public class JobProgressHub : Hub
    {
        public async Task AssociateJob(string jobId)
        {
            await Groups.Add(Context.ConnectionId, jobId);
        }

        public static void SendProgressMessage(String message, string jobId)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<JobProgressHub>();
            context.Clients.Group(jobId).progressMessage(message);
        }

        public static void SendRedirect(String controller, String action, int id, String jobId)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<JobProgressHub>();
            context.Clients.Group(jobId).redirect(controller, action, id);
        }
    }
}