namespace ValetService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class organizationId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tags", name: "Organization_Id", newName: "OrganizationId");
            RenameColumn(table: "dbo.Zones", name: "Organization_Id", newName: "OrganizationId");
            RenameIndex(table: "dbo.Tags", name: "IX_Organization_Id", newName: "IX_OrganizationId");
            RenameIndex(table: "dbo.Zones", name: "IX_Organization_Id", newName: "IX_OrganizationId");
            AddColumn("dbo.FeeRates", "OrganizationId", c => c.String());
            AddColumn("dbo.Tickets", "OrganizationId", c => c.String());
            AddColumn("dbo.Vehicles", "OrganizationId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "OrganizationId");
            DropColumn("dbo.Tickets", "OrganizationId");
            DropColumn("dbo.FeeRates", "OrganizationId");
            RenameIndex(table: "dbo.Zones", name: "IX_OrganizationId", newName: "IX_Organization_Id");
            RenameIndex(table: "dbo.Tags", name: "IX_OrganizationId", newName: "IX_Organization_Id");
            RenameColumn(table: "dbo.Zones", name: "OrganizationId", newName: "Organization_Id");
            RenameColumn(table: "dbo.Tags", name: "OrganizationId", newName: "Organization_Id");
        }
    }
}
