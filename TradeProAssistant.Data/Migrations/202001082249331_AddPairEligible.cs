namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPairEligible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "PairEligible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "PairEligible");
        }
    }
}
