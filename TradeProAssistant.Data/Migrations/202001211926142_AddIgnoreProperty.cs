namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIgnoreProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "Ignore", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "Ignore");
        }
    }
}
