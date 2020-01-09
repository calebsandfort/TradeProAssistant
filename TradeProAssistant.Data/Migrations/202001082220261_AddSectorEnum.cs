namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSectorEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "SectorEnum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "SectorEnum");
        }
    }
}
