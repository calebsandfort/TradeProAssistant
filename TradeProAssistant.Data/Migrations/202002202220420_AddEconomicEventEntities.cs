namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEconomicEventEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EconomicDays",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Identifier);
            
            CreateTable(
                "dbo.EconomicEvents",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        EventType = c.Int(nullable: false),
                        EconomicDayIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.EconomicDays", t => t.EconomicDayIdentifier)
                .Index(t => t.EconomicDayIdentifier);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EconomicEvents", "EconomicDayIdentifier", "dbo.EconomicDays");
            DropIndex("dbo.EconomicEvents", new[] { "EconomicDayIdentifier" });
            DropTable("dbo.EconomicEvents");
            DropTable("dbo.EconomicDays");
        }
    }
}
