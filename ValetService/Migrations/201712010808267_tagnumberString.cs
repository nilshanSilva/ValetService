namespace ValetService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tagnumberString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "TagNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "TagNumber", c => c.Int(nullable: false));
        }
    }
}
