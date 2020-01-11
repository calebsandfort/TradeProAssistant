namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOptionChain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calls",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        StrikePrice = c.DateTime(nullable: false),
                        Bid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ask = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OptionDateIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.OptionDates", t => t.OptionDateIdentifier)
                .Index(t => t.OptionDateIdentifier);
            
            CreateTable(
                "dbo.OptionDates",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        ExpiryDate = c.DateTime(nullable: false),
                        OptionChain_Identifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.OptionChains", t => t.OptionChain_Identifier)
                .Index(t => t.OptionChain_Identifier);
            
            CreateTable(
                "dbo.Puts",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        StrikePrice = c.DateTime(nullable: false),
                        Bid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ask = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OptionDateIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.OptionDates", t => t.OptionDateIdentifier)
                .Index(t => t.OptionDateIdentifier);
            
            CreateTable(
                "dbo.OptionChains",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SecurityIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.Securities", t => t.SecurityIdentifier)
                .Index(t => t.SecurityIdentifier);
            
            AddColumn("dbo.Securities", "CurrentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OptionChains", "SecurityIdentifier", "dbo.Securities");
            DropForeignKey("dbo.OptionDates", "OptionChain_Identifier", "dbo.OptionChains");
            DropForeignKey("dbo.Puts", "OptionDateIdentifier", "dbo.OptionDates");
            DropForeignKey("dbo.Calls", "OptionDateIdentifier", "dbo.OptionDates");
            DropIndex("dbo.OptionChains", new[] { "SecurityIdentifier" });
            DropIndex("dbo.Puts", new[] { "OptionDateIdentifier" });
            DropIndex("dbo.OptionDates", new[] { "OptionChain_Identifier" });
            DropIndex("dbo.Calls", new[] { "OptionDateIdentifier" });
            DropColumn("dbo.Securities", "CurrentPrice");
            DropTable("dbo.OptionChains");
            DropTable("dbo.Puts");
            DropTable("dbo.OptionDates");
            DropTable("dbo.Calls");
        }
    }
}
