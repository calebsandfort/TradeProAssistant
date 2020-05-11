namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRiskParameters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RiskParameters",
                c => new
                    {
                        Identifier = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Active = c.Boolean(nullable: false),
                        DailyTarget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeeklyTarget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MonthlyTarget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DailyStop = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeeklyStop = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MonthlyStop = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Identifier);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RiskParameters");
        }
    }
}
