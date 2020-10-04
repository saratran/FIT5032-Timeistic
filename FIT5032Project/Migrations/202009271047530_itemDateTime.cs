namespace FIT5032Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "EndTime");
            DropColumn("dbo.Items", "StartTime");
        }
    }
}
