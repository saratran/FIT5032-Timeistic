namespace FIT5032Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "Name", c => c.String());
            AlterColumn("dbo.Locations", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "Name", c => c.String(nullable: false));
        }
    }
}
