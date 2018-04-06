namespace PhotoGallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PathAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhotoEntities", "Path", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhotoEntities", "Path");
        }
    }
}
