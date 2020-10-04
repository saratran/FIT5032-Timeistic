namespace FIT5032Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemDateTime1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Date");
        }
    }
}
