namespace FIT5032Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskRename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Tasks", newName: "Items");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Items", newName: "Tasks");
        }
    }
}
