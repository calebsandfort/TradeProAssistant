namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBullishBearishProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "IsBullish", c => c.Boolean(nullable: false));
            AddColumn("dbo.Securities", "IsBearish", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "IsBearish");
            DropColumn("dbo.Securities", "IsBullish");
        }
    }
}
