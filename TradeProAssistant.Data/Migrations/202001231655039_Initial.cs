namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BearCallSpreads",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        SellStrike = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyStrike = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellCallIdentifier = c.Int(),
                        BuyCallIdentifier = c.Int(),
                        SecurityIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.Calls", t => t.BuyCallIdentifier)
                .ForeignKey("dbo.Securities", t => t.SecurityIdentifier)
                .ForeignKey("dbo.Calls", t => t.SellCallIdentifier)
                .Index(t => t.SellCallIdentifier)
                .Index(t => t.BuyCallIdentifier)
                .Index(t => t.SecurityIdentifier);
            
            CreateTable(
                "dbo.Calls",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Bid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ask = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Identifier);
            
            CreateTable(
                "dbo.Securities",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Symbol = c.String(maxLength: 5),
                        MarketCap = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sector = c.String(maxLength: 50),
                        AssetClass = c.String(maxLength: 50),
                        PairEligible = c.Boolean(nullable: false),
                        Ignore = c.Boolean(nullable: false),
                        IsBullish = c.Boolean(nullable: false),
                        IsBearish = c.Boolean(nullable: false),
                        ExDividendDate = c.DateTime(),
                        NextEarningsDate = c.DateTime(),
                        SectorEnum = c.Int(nullable: false),
                        AssetClassEnum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Identifier);
            
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
            
            CreateTable(
                "dbo.Puts",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Bid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ask = c.Decimal(nullable: false, precision: 18, scale: 2),
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
            
            CreateTable(
                "dbo.BullPutSpreads",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        SellStrike = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyStrike = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellPutIdentifier = c.Int(),
                        BuyPutIdentifier = c.Int(),
                        SecurityIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.Puts", t => t.BuyPutIdentifier)
                .ForeignKey("dbo.Securities", t => t.SecurityIdentifier)
                .ForeignKey("dbo.Puts", t => t.SellPutIdentifier)
                .Index(t => t.SellPutIdentifier)
                .Index(t => t.BuyPutIdentifier)
                .Index(t => t.SecurityIdentifier);
            
            CreateTable(
                "dbo.PairCondors",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Dummy = c.Int(nullable: false),
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
                        Dummy = c.Int(nullable: false),
                        GenerationMethod = c.Int(nullable: false),
                        PlaySheetIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.WeeklyIncomePlaySheets", t => t.PlaySheetIdentifier)
                .Index(t => t.PlaySheetIdentifier);
            
            CreateTable(
                "dbo.WeeklyIncomePlaySheets",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        Expiry = c.DateTime(nullable: false),
                        Used = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Identifier);
            
            CreateTable(
                "dbo.WeeklyIncomeComboCounts",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        SectorEnum = c.Int(nullable: false),
                        PlaySheetIdentifier = c.Int(),
                    })
                .PrimaryKey(t => t.Identifier)
                .ForeignKey("dbo.WeeklyIncomePlaySheets", t => t.PlaySheetIdentifier)
                .Index(t => t.PlaySheetIdentifier);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeeklyIncomeActionPlans", "PlaySheetIdentifier", "dbo.WeeklyIncomePlaySheets");
            DropForeignKey("dbo.WeeklyIncomeComboCounts", "PlaySheetIdentifier", "dbo.WeeklyIncomePlaySheets");
            DropForeignKey("dbo.PairCondors", "WeeklyIncomeActionPlan_Identifier", "dbo.WeeklyIncomeActionPlans");
            DropForeignKey("dbo.PairCondors", "BullPutSpreadIdentifier", "dbo.BullPutSpreads");
            DropForeignKey("dbo.PairCondors", "BearCallSpreadIdentifier", "dbo.BearCallSpreads");
            DropForeignKey("dbo.BullPutSpreads", "SellPutIdentifier", "dbo.Puts");
            DropForeignKey("dbo.BullPutSpreads", "SecurityIdentifier", "dbo.Securities");
            DropForeignKey("dbo.BullPutSpreads", "BuyPutIdentifier", "dbo.Puts");
            DropForeignKey("dbo.BearCallSpreads", "SellCallIdentifier", "dbo.Calls");
            DropForeignKey("dbo.BearCallSpreads", "SecurityIdentifier", "dbo.Securities");
            DropForeignKey("dbo.WeekCandlesticks", "Security_Identifier", "dbo.Securities");
            DropForeignKey("dbo.OptionChains", "SecurityIdentifier", "dbo.Securities");
            DropForeignKey("dbo.OptionDates", "OptionChain_Identifier", "dbo.OptionChains");
            DropForeignKey("dbo.OptionStrikes", "PutIdentifier", "dbo.Puts");
            DropForeignKey("dbo.OptionStrikes", "OptionDateIdentifier", "dbo.OptionDates");
            DropForeignKey("dbo.OptionStrikes", "CallIdentifier", "dbo.Calls");
            DropForeignKey("dbo.DayCandlesticks", "Security_Identifier", "dbo.Securities");
            DropForeignKey("dbo.BearCallSpreads", "BuyCallIdentifier", "dbo.Calls");
            DropIndex("dbo.WeeklyIncomeComboCounts", new[] { "PlaySheetIdentifier" });
            DropIndex("dbo.WeeklyIncomeActionPlans", new[] { "PlaySheetIdentifier" });
            DropIndex("dbo.PairCondors", new[] { "WeeklyIncomeActionPlan_Identifier" });
            DropIndex("dbo.PairCondors", new[] { "BearCallSpreadIdentifier" });
            DropIndex("dbo.PairCondors", new[] { "BullPutSpreadIdentifier" });
            DropIndex("dbo.BullPutSpreads", new[] { "SecurityIdentifier" });
            DropIndex("dbo.BullPutSpreads", new[] { "BuyPutIdentifier" });
            DropIndex("dbo.BullPutSpreads", new[] { "SellPutIdentifier" });
            DropIndex("dbo.WeekCandlesticks", new[] { "Security_Identifier" });
            DropIndex("dbo.OptionStrikes", new[] { "PutIdentifier" });
            DropIndex("dbo.OptionStrikes", new[] { "CallIdentifier" });
            DropIndex("dbo.OptionStrikes", new[] { "OptionDateIdentifier" });
            DropIndex("dbo.OptionDates", new[] { "OptionChain_Identifier" });
            DropIndex("dbo.OptionChains", new[] { "SecurityIdentifier" });
            DropIndex("dbo.DayCandlesticks", new[] { "Security_Identifier" });
            DropIndex("dbo.BearCallSpreads", new[] { "SecurityIdentifier" });
            DropIndex("dbo.BearCallSpreads", new[] { "BuyCallIdentifier" });
            DropIndex("dbo.BearCallSpreads", new[] { "SellCallIdentifier" });
            DropTable("dbo.WeeklyIncomeComboCounts");
            DropTable("dbo.WeeklyIncomePlaySheets");
            DropTable("dbo.WeeklyIncomeActionPlans");
            DropTable("dbo.PairCondors");
            DropTable("dbo.BullPutSpreads");
            DropTable("dbo.WeekCandlesticks");
            DropTable("dbo.Puts");
            DropTable("dbo.OptionStrikes");
            DropTable("dbo.OptionDates");
            DropTable("dbo.OptionChains");
            DropTable("dbo.DayCandlesticks");
            DropTable("dbo.Securities");
            DropTable("dbo.Calls");
            DropTable("dbo.BearCallSpreads");
        }
    }
}
