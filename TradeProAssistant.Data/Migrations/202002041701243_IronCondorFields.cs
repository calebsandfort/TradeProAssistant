namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IronCondorFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "Support", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Securities", "Resistance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Securities", "IronCondorEligible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "IronCondorEligible");
            DropColumn("dbo.Securities", "Resistance");
            DropColumn("dbo.Securities", "Support");
        }
    }
}
