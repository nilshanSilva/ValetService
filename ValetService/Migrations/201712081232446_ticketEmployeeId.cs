namespace ValetService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketEmployeeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "TicketCloser_Id", "dbo.Employees");
            DropForeignKey("dbo.Tickets", "TicketRaiser_Id", "dbo.Employees");
            DropIndex("dbo.Tickets", new[] { "TicketCloser_Id" });
            DropIndex("dbo.Tickets", new[] { "TicketRaiser_Id" });
            AddColumn("dbo.Tickets", "TicketRaiserId", c => c.String(nullable: false));
            AddColumn("dbo.Tickets", "TicketCloserId", c => c.String());
            DropColumn("dbo.Tickets", "TicketCloser_Id");
            DropColumn("dbo.Tickets", "TicketRaiser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "TicketRaiser_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Tickets", "TicketCloser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Tickets", "TicketCloserId");
            DropColumn("dbo.Tickets", "TicketRaiserId");
            CreateIndex("dbo.Tickets", "TicketRaiser_Id");
            CreateIndex("dbo.Tickets", "TicketCloser_Id");
            AddForeignKey("dbo.Tickets", "TicketRaiser_Id", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "TicketCloser_Id", "dbo.Employees", "Id");
        }
    }
}
