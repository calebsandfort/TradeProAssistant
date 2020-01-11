namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReworkOptionChain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Calls", "OptionDateIdentifier", "dbo.OptionDates");
            DropForeignKey("dbo.Puts", "OptionDateIdentifier", "dbo.OptionDates");
            DropIndex("dbo.Calls", new[] { "OptionDateIdentifier" });
            DropIndex("dbo.Puts", new[] { "OptionDateIdentifier" });
            CreateTable(
                "dbo.OptionStrikes",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        StrikePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OptionDateIdentifier = c.Int(),
                        CallIdentifier = c.Int(),
                        PutIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.Calls", t => t.CallIdentifier)
                .ForeignKey("dbo.OptionDates", t => t.OptionDateIdentifier)
                .ForeignKey("dbo.Puts", t => t.PutIdentifier)
                .Index(t => t.OptionDateIdentifier)
                .Index(t => t.CallIdentifier)
                .Index(t => t.PutIdentifier);
            
            DropColumn("dbo.Calls", "StrikePrice");
            DropColumn("dbo.Calls", "OptionDateIdentifier");
            DropColumn("dbo.Puts", "StrikePrice");
            DropColumn("dbo.Puts", "OptionDateIdentifier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Puts", "OptionDateIdentifier", c => c.Int());
            AddColumn("dbo.Puts", "StrikePrice", c => c.DateTime(nullable: false));
            AddColumn("dbo.Calls", "OptionDateIdentifier", c => c.Int());
            AddColumn("dbo.Calls", "StrikePrice", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.OptionStrikes", "PutIdentifier", "dbo.Puts");
            DropForeignKey("dbo.OptionStrikes", "OptionDateIdentifier", "dbo.OptionDates");
            DropForeignKey("dbo.OptionStrikes", "CallIdentifier", "dbo.Calls");
            DropIndex("dbo.OptionStrikes", new[] { "PutIdentifier" });
            DropIndex("dbo.OptionStrikes", new[] { "CallIdentifier" });
            DropIndex("dbo.OptionStrikes", new[] { "OptionDateIdentifier" });
            DropTable("dbo.OptionStrikes");
            CreateIndex("dbo.Puts", "OptionDateIdentifier");
            CreateIndex("dbo.Calls", "OptionDateIdentifier");
            AddForeignKey("dbo.Puts", "OptionDateIdentifier", "dbo.OptionDates", "Identifier");
            AddForeignKey("dbo.Calls", "OptionDateIdentifier", "dbo.OptionDates", "Identifier");
        }
    }
}
