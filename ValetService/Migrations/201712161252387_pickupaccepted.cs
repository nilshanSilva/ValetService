namespace ValetService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pickupaccepted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "PickupAccepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "PickupAccepted");
        }
    }
}
