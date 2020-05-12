namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStrategyToTradeTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PullbackTradeTickets", "Strategy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PullbackTradeTickets", "Strategy");
        }
    }
}
