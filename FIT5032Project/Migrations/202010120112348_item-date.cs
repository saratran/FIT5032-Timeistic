namespace FIT5032Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Date");
            DropColumn("dbo.Items", "EndTime");
            DropColumn("dbo.Items", "StartTime");
        }
    }
}
