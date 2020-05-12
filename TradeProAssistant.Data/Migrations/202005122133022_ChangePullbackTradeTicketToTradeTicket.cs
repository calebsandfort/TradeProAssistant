namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePullbackTradeTicketToTradeTicket : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PullbackTradeTickets", newName: "TradeTickets");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TradeTickets", newName: "PullbackTradeTickets");
        }
    }
}
