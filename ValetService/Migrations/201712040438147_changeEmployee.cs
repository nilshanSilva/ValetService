namespace ValetService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeEmployee : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Employees", name: "Organization_Id", newName: "OrganizationId");
            RenameIndex(table: "dbo.Employees", name: "IX_Organization_Id", newName: "IX_OrganizationId");
            AddColumn("dbo.Employees", "IsLoggedIn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "IsLoggedIn");
            RenameIndex(table: "dbo.Employees", name: "IX_OrganizationId", newName: "IX_Organization_Id");
            RenameColumn(table: "dbo.Employees", name: "OrganizationId", newName: "Organization_Id");
        }
    }
}
