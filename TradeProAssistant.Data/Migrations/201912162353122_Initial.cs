namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayCandlesticks",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Open = c.Decimal(nullable: false, precision: 18, scale: 2),
                        High = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Low = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Close = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Volume = c.Int(nullable: false),
                        Security_Identifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.Securities", t => t.Security_Identifier)
                .Index(t => t.Security_Identifier);
            
            CreateTable(
                "dbo.Securities",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Symbol = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.Identifier);
            
            CreateTable(
                "dbo.WeekCandlesticks",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Open = c.Decimal(nullable: false, precision: 18, scale: 2),
                        High = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Low = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Close = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Volume = c.Int(nullable: false),
                        Security_Identifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.Securities", t => t.Security_Identifier)
                .Index(t => t.Security_Identifier);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeekCandlesticks", "Security_Identifier", "dbo.Securities");
            DropForeignKey("dbo.DayCandlesticks", "Security_Identifier", "dbo.Securities");
            DropIndex("dbo.WeekCandlesticks", new[] { "Security_Identifier" });
            DropIndex("dbo.DayCandlesticks", new[] { "Security_Identifier" });
            DropTable("dbo.WeekCandlesticks");
            DropTable("dbo.Securities");
            DropTable("dbo.DayCandlesticks");
        }
    }
}
