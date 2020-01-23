namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsedPropertyToPlaySheet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WeeklyIncomePlaySheets", "Used", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WeeklyIncomePlaySheets", "Used");
        }
    }
}
