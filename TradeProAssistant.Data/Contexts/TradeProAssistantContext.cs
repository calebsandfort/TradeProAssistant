using Entities;
using System.Data.Entity;

namespace Contexts
{
	public class TradeProAssistantContext : DbContext
	{
		public DbSet<Security> Securities { get; set; }
		public DbSet<DayCandlestick> DayCandlesticks { get; set; }
		public DbSet<WeekCandlestick> WeekCandlesticks { get; set; }
		public DbSet<Call> Calls { get; set; }
		public DbSet<Put> Puts { get; set; }
		public DbSet<OptionStrike> OptionStrikes { get; set; }
		public DbSet<OptionDate> OptionDates { get; set; }
		public DbSet<OptionChain> OptionChains { get; set; }
		public DbSet<WeeklyIncomeActionPlan> WeeklyIncomeActionPlans { get; set; }
		public DbSet<PairCondor> PairCondors { get; set; }
		public DbSet<BullPutSpread> BullPutSpreads { get; set; }
		public DbSet<BearCallSpread> BearCallSpreads { get; set; }
		
		public TradeProAssistantContext() : base("TradeProAssistant") {}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
	}
}
