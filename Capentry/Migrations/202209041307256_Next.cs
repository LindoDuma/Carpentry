namespace Capentry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Next : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Images", new[] { "ProjectId" });
            CreateIndex("dbo.Images", "ProjectID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Images", new[] { "ProjectID" });
            CreateIndex("dbo.Images", "ProjectId");
        }
    }
}
