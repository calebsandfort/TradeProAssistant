namespace TradeProAssistant.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PairCondors", "Strategy", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.PairCondors", "Strategy");
        }
    }
}
