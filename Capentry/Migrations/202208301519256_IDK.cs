namespace Capentry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDK : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        ProjectYear = c.Int(nullable: false),
                        ProjectType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            AddColumn("dbo.Images", "ProjectID", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "ProjectID");
            AddForeignKey("dbo.Images", "ProjectID", "dbo.Projects", "ProjectId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "ProjectID", "dbo.Projects");
            DropIndex("dbo.Images", new[] { "ProjectID" });
            DropColumn("dbo.Images", "ProjectID");
            DropTable("dbo.Projects");
        }
    }
}
