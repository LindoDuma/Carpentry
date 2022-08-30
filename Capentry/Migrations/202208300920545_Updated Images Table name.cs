namespace Capentry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedImagesTablename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ImageModels", newName: "Images");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Images", newName: "ImageModels");
        }
    }
}
