namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustNotesSize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PullbackTradeTickets", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PullbackTradeTickets", "Notes", c => c.String(maxLength: 100));
        }
    }
}
