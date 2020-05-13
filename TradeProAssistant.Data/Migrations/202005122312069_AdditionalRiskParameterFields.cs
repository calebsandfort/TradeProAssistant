namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalRiskParameterFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RiskParameters", "TpaDailyTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RiskParameters", "TpaWeeklyTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RiskParameters", "TpaMonthlyTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RiskParameters", "MyDailyTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RiskParameters", "MyWeeklyTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RiskParameters", "MyMonthlyTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.RiskParameters", "DailyTarget");
            DropColumn("dbo.RiskParameters", "WeeklyTarget");
            DropColumn("dbo.RiskParameters", "MonthlyTarget");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RiskParameters", "MonthlyTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RiskParameters", "WeeklyTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.RiskParameters", "DailyTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.RiskParameters", "MyMonthlyTarget");
            DropColumn("dbo.RiskParameters", "MyWeeklyTarget");
            DropColumn("dbo.RiskParameters", "MyDailyTarget");
            DropColumn("dbo.RiskParameters", "TpaMonthlyTarget");
            DropColumn("dbo.RiskParameters", "TpaWeeklyTarget");
            DropColumn("dbo.RiskParameters", "TpaDailyTarget");
        }
    }
}
