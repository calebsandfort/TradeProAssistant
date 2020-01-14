namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImportantDates2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Securities", "ExDividendDate", c => c.DateTime());
            AddColumn("dbo.Securities", "NextEarningsDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Securities", "NextEarningsDate");
            DropColumn("dbo.Securities", "ExDividendDate");
        }
    }
}
