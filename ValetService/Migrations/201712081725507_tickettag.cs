namespace ValetService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tickettag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Vehicles", "Id", "dbo.Tickets");
            DropForeignKey("dbo.Vehicles", "Zone_Id", "dbo.Zones");
            DropIndex("dbo.Tickets", new[] { "Tag_Id" });
            DropIndex("dbo.Vehicles", new[] { "Id" });
            DropIndex("dbo.Vehicles", new[] { "Zone_Id" });
            AddColumn("dbo.Tickets", "TagId", c => c.String());
            AddColumn("dbo.Tickets", "VehicleId", c => c.String());
            AddColumn("dbo.Vehicles", "ZoneId", c => c.String());
            AddColumn("dbo.Vehicles", "TicketId", c => c.String(nullable: false));
            DropColumn("dbo.Tickets", "Tag_Id");
            DropColumn("dbo.Vehicles", "Zone_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Zone_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Tickets", "Tag_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Vehicles", "TicketId");
            DropColumn("dbo.Vehicles", "ZoneId");
            DropColumn("dbo.Tickets", "VehicleId");
            DropColumn("dbo.Tickets", "TagId");
            CreateIndex("dbo.Vehicles", "Zone_Id");
            CreateIndex("dbo.Vehicles", "Id");
            CreateIndex("dbo.Tickets", "Tag_Id");
            AddForeignKey("dbo.Vehicles", "Zone_Id", "dbo.Zones", "Id");
            AddForeignKey("dbo.Vehicles", "Id", "dbo.Tickets", "Id");
            AddForeignKey("dbo.Tickets", "Tag_Id", "dbo.Tags", "Id");
        }
    }
}
