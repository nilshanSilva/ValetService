namespace ValetService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketOpenedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "OpenedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "OpenedAt");
        }
    }
}
