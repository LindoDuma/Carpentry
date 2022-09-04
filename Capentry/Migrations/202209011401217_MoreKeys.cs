namespace Capentry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreKeys : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Images", new[] { "ProjectID" });
            CreateIndex("dbo.Images", "ProjectId");
            AddForeignKey("dbo.Images", "ProjectID", "dbo.Projects", "ProjectId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Images", new[] { "ProjectId" });
            CreateIndex("dbo.Images", "ProjectID");
        }
    }
}
