namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssetClassEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "AssetClassEnum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "AssetClassEnum");
        }
    }
}
