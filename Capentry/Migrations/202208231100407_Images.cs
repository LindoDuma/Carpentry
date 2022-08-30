namespace Capentry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Images : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        ImagePath = c.String(),
                        PublicID = c.String(),
                    })
                .PrimaryKey(t => t.ImageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageModels");
        }
    }
}
