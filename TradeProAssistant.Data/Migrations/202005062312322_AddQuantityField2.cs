namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuantityField2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PullbackTradeTickets", "Quantity", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PullbackTradeTickets", "Quantity");
        }
    }
}
