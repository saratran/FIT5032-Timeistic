namespace FIT5032Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemrating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Rating", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Rating");
        }
    }
}
