namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStrategyType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WeeklyIncomePlaySheets", "Strategy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WeeklyIncomePlaySheets", "Strategy");
        }
    }
}
