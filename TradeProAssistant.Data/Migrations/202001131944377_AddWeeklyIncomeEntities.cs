namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeeklyIncomeEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BearCallSpreads",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Risk = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        SellStrike = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyStrike = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SecurityIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.Securities", t => t.SecurityIdentifier)
                .Index(t => t.SecurityIdentifier);
            
            CreateTable(
                "dbo.BullPutSpreads",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Risk = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        SellStrike = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyStrike = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SecurityIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.Securities", t => t.SecurityIdentifier)
                .Index(t => t.SecurityIdentifier);
            
            CreateTable(
                "dbo.PairCondors",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Risk = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SectorEnum = c.Int(nullable: false),
                        BullPutSpreadIdentifier = c.Int(),
                        BearCallSpreadIdentifier = c.Int(),
                        WeeklyIncomeActionPlan_Identifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.BearCallSpreads", t => t.BearCallSpreadIdentifier)
                .ForeignKey("dbo.BullPutSpreads", t => t.BullPutSpreadIdentifier)
                .ForeignKey("dbo.WeeklyIncomeActionPlans", t => t.WeeklyIncomeActionPlan_Identifier)
                .Index(t => t.BullPutSpreadIdentifier)
                .Index(t => t.BearCallSpreadIdentifier)
                .Index(t => t.WeeklyIncomeActionPlan_Identifier);
            
            CreateTable(
                "dbo.WeeklyIncomeActionPlans",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Risk = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TimeStamp = c.DateTime(nullable: false),
                        Expiry = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Identifier);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PairCondors", "WeeklyIncomeActionPlan_Identifier", "dbo.WeeklyIncomeActionPlans");
            DropForeignKey("dbo.PairCondors", "BullPutSpreadIdentifier", "dbo.BullPutSpreads");
            DropForeignKey("dbo.PairCondors", "BearCallSpreadIdentifier", "dbo.BearCallSpreads");
            DropForeignKey("dbo.BullPutSpreads", "SecurityIdentifier", "dbo.Securities");
            DropForeignKey("dbo.BearCallSpreads", "SecurityIdentifier", "dbo.Securities");
            DropIndex("dbo.PairCondors", new[] { "WeeklyIncomeActionPlan_Identifier" });
            DropIndex("dbo.PairCondors", new[] { "BearCallSpreadIdentifier" });
            DropIndex("dbo.PairCondors", new[] { "BullPutSpreadIdentifier" });
            DropIndex("dbo.BullPutSpreads", new[] { "SecurityIdentifier" });
            DropIndex("dbo.BearCallSpreads", new[] { "SecurityIdentifier" });
            DropTable("dbo.WeeklyIncomeActionPlans");
            DropTable("dbo.PairCondors");
            DropTable("dbo.BullPutSpreads");
            DropTable("dbo.BearCallSpreads");
        }
    }
}
