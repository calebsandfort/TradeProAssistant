namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNbtField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradeTickets", "NBT", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TradeTickets", "NBT");
        }
    }
}
