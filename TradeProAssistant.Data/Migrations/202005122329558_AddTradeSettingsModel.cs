namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTradeSettingsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TradingSettings",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        MonthStart = c.DateTime(nullable: false),
                        RiskParametersIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.RiskParameters", t => t.RiskParametersIdentifier)
                .Index(t => t.RiskParametersIdentifier);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TradingSettings", "RiskParametersIdentifier", "dbo.RiskParameters");
            DropIndex("dbo.TradingSettings", new[] { "RiskParametersIdentifier" });
            DropTable("dbo.TradingSettings");
        }
    }
}
