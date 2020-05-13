namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMarketStructureQualifiers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradeTickets", "MarketStructureQualified1", c => c.Boolean(nullable: false));
            AddColumn("dbo.TradeTickets", "MarketStructureQualified2", c => c.Boolean(nullable: false));
            AddColumn("dbo.TradeTickets", "MarketStructureQualified3", c => c.Boolean(nullable: false));
            DropColumn("dbo.TradeTickets", "ZoneQualified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TradeTickets", "ZoneQualified", c => c.Boolean(nullable: false));
            DropColumn("dbo.TradeTickets", "MarketStructureQualified3");
            DropColumn("dbo.TradeTickets", "MarketStructureQualified2");
            DropColumn("dbo.TradeTickets", "MarketStructureQualified1");
        }
    }
}
