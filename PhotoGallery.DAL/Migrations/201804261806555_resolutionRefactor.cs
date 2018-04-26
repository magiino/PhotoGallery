namespace PhotoGallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resolutionRefactor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResolutionEntities", "Resolution", c => c.String());
            DropColumn("dbo.ResolutionEntities", "Height");
            DropColumn("dbo.ResolutionEntities", "Width");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResolutionEntities", "Width", c => c.Int(nullable: false));
            AddColumn("dbo.ResolutionEntities", "Height", c => c.Int(nullable: false));
            DropColumn("dbo.ResolutionEntities", "Resolution");
        }
    }
}
