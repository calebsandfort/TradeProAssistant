namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFuturesContract : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candlesticks",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Open = c.Decimal(nullable: false, precision: 18, scale: 2),
                        High = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Low = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Close = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Volume = c.Int(nullable: false),
                        Interval = c.Int(nullable: false),
                        FutureContractIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.FutureContracts", t => t.FutureContractIdentifier)
                .Index(t => t.FutureContractIdentifier);
            
            CreateTable(
                "dbo.FutureContracts",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Symbol = c.String(maxLength: 2),
                        TickValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TickSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Identifier);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Candlesticks", "FutureContractIdentifier", "dbo.FutureContracts");
            DropIndex("dbo.Candlesticks", new[] { "FutureContractIdentifier" });
            DropTable("dbo.FutureContracts");
            DropTable("dbo.Candlesticks");
        }
    }
}
