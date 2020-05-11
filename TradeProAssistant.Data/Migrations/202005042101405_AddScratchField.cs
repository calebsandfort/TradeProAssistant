namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScratchField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PullbackTradeTickets", "Scratch", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PullbackTradeTickets", "Scratch");
        }
    }
}
