namespace FIT5032Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemlocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Location_Id", c => c.Int());
            CreateIndex("dbo.Items", "Location_Id");
            AddForeignKey("dbo.Items", "Location_Id", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Items", new[] { "Location_Id" });
            DropColumn("dbo.Items", "Location_Id");
        }
    }
}
