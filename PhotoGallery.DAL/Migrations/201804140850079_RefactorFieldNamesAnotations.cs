namespace PhotoGallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorFieldNamesAnotations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TagEntities", "XPosition", c => c.Int(nullable: false));
            AddColumn("dbo.TagEntities", "YPosition", c => c.Int(nullable: false));
            DropColumn("dbo.TagEntities", "PositionOnPhotoX");
            DropColumn("dbo.TagEntities", "PositionOnPhotoY");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TagEntities", "PositionOnPhotoY", c => c.Int(nullable: false));
            AddColumn("dbo.TagEntities", "PositionOnPhotoX", c => c.Int(nullable: false));
            DropColumn("dbo.TagEntities", "YPosition");
            DropColumn("dbo.TagEntities", "XPosition");
        }
    }
}
