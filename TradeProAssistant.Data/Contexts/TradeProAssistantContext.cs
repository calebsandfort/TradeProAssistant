using Entities;
using System.Data.Entity;

namespace Contexts
{
	public class TradeProAssistantContext : DbContext
	{
		public DbSet<Security> Securities { get; set; }
		public DbSet<DayCandlestick> DayCandlesticks { get; set; }
		public DbSet<WeekCandlestick> WeekCandlesticks { get; set; }

        public TradeProAssistantContext() : base("TradeProAssistant")
        {
        }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
