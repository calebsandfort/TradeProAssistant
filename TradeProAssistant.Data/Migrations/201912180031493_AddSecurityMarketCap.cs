namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSecurityMarketCap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "MarketCap", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "MarketCap");
        }
    }
}
