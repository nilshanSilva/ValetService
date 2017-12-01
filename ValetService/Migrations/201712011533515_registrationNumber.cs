namespace ValetService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registrationNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "RegistrationNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "RegistrationNumber");
        }
    }
}
