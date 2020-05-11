namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPnLField : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PullbackTradeTickets",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        ZoneQualified = c.Boolean(nullable: false),
                        Qualifier1Disqualified = c.Boolean(nullable: false),
                        Qualifier2Disqualified = c.Boolean(nullable: false),
                        Qualifier3Disqualified = c.Boolean(nullable: false),
                        Qualifier4Disqualified = c.Boolean(nullable: false),
                        Notes = c.String(maxLength: 100),
                        Won = c.Boolean(nullable: false),
                        PnL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Asset = c.Int(nullable: false),
                        Qualifier1 = c.Int(nullable: false),
                        Qualifier2 = c.Int(nullable: false),
                        Qualifier3 = c.Int(nullable: false),
                        Qualifier4 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Identifier);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PullbackTradeTickets");
        }
    }
}
