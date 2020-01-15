namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustSpreadEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BearCallSpreads", "SellCallIdentifier", c => c.Int());
            AddColumn("dbo.BearCallSpreads", "BuyCallIdentifier", c => c.Int());
            AddColumn("dbo.BullPutSpreads", "SellPutIdentifier", c => c.Int());
            AddColumn("dbo.BullPutSpreads", "BuyPutIdentifier", c => c.Int());
            CreateIndex("dbo.BearCallSpreads", "SellCallIdentifier");
            CreateIndex("dbo.BearCallSpreads", "BuyCallIdentifier");
            CreateIndex("dbo.BullPutSpreads", "SellPutIdentifier");
            CreateIndex("dbo.BullPutSpreads", "BuyPutIdentifier");
            AddForeignKey("dbo.BearCallSpreads", "BuyCallIdentifier", "dbo.Calls", "Identifier");
            AddForeignKey("dbo.BearCallSpreads", "SellCallIdentifier", "dbo.Calls", "Identifier");
            AddForeignKey("dbo.BullPutSpreads", "BuyPutIdentifier", "dbo.Puts", "Identifier");
            AddForeignKey("dbo.BullPutSpreads", "SellPutIdentifier", "dbo.Puts", "Identifier");
            DropColumn("dbo.BearCallSpreads", "SellStrike");
            DropColumn("dbo.BearCallSpreads", "BuyStrike");
            DropColumn("dbo.BullPutSpreads", "SellStrike");
            DropColumn("dbo.BullPutSpreads", "BuyStrike");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BullPutSpreads", "BuyStrike", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BullPutSpreads", "SellStrike", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BearCallSpreads", "BuyStrike", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BearCallSpreads", "SellStrike", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.BullPutSpreads", "SellPutIdentifier", "dbo.Puts");
            DropForeignKey("dbo.BullPutSpreads", "BuyPutIdentifier", "dbo.Puts");
            DropForeignKey("dbo.BearCallSpreads", "SellCallIdentifier", "dbo.Calls");
            DropForeignKey("dbo.BearCallSpreads", "BuyCallIdentifier", "dbo.Calls");
            DropIndex("dbo.BullPutSpreads", new[] { "BuyPutIdentifier" });
            DropIndex("dbo.BullPutSpreads", new[] { "SellPutIdentifier" });
            DropIndex("dbo.BearCallSpreads", new[] { "BuyCallIdentifier" });
            DropIndex("dbo.BearCallSpreads", new[] { "SellCallIdentifier" });
            DropColumn("dbo.BullPutSpreads", "BuyPutIdentifier");
            DropColumn("dbo.BullPutSpreads", "SellPutIdentifier");
            DropColumn("dbo.BearCallSpreads", "BuyCallIdentifier");
            DropColumn("dbo.BearCallSpreads", "SellCallIdentifier");
        }
    }
}
