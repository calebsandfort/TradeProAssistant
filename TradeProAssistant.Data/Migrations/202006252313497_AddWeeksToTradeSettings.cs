namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeeksToTradeSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradingSettings", "Weeks", c => c.Int(nullable: false, defaultValue: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TradingSettings", "Weeks");
        }
    }
}
