using Hangfire;
using Microsoft.Owin;
using Owin;
using TradeProAssistant;
using TradeProAssistant.Utilities;

[assembly: OwinStartup(typeof(Startup))]
namespace TradeProAssistant
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireDb");

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });

            app.UseHangfireServer();
            app.MapSignalR();
        }
    }
}