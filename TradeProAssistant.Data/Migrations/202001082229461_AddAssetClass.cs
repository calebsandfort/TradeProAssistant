namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssetClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "AssetClass", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "AssetClass");
        }
    }
}
