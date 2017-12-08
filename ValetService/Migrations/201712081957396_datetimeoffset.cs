namespace ValetService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimeoffset : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "ClosedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "ClosedAt", c => c.DateTime(nullable: false));
        }
    }
}
