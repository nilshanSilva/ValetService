namespace ValetService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketAssigner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "PickupAssignerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "PickupAssignerId");
        }
    }
}
