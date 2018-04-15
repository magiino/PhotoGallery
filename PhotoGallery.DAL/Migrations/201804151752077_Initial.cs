namespace PhotoGallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CoverPhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhotoEntities", t => t.CoverPhotoId, cascadeDelete: true)
                .Index(t => t.CoverPhotoId);
            
            CreateTable(
                "dbo.PhotoEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Path = c.String(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        Format = c.Int(nullable: false),
                        ResolutionId = c.Int(nullable: false),
                        Note = c.String(),
                        Location = c.String(),
                        AlbumId = c.Int(nullable: false),
                        AlbumEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlbumEntities", t => t.AlbumId)
                .ForeignKey("dbo.ResolutionEntities", t => t.ResolutionId)
                .ForeignKey("dbo.AlbumEntities", t => t.AlbumEntity_Id)
                .Index(t => t.ResolutionId)
                .Index(t => t.AlbumId)
                .Index(t => t.AlbumEntity_Id);
            
            CreateTable(
                "dbo.ResolutionEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        XPosition = c.Int(nullable: false),
                        YPosition = c.Int(nullable: false),
                        Name = c.String(),
                        PersonId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonEntities", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.PersonEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagEntityPhotoEntities",
                c => new
                    {
                        TagEntity_Id = c.Int(nullable: false),
                        PhotoEntity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagEntity_Id, t.PhotoEntity_Id })
                .ForeignKey("dbo.TagEntities", t => t.TagEntity_Id)
                .ForeignKey("dbo.PhotoEntities", t => t.PhotoEntity_Id)
                .Index(t => t.TagEntity_Id)
                .Index(t => t.PhotoEntity_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoEntities", "AlbumEntity_Id", "dbo.AlbumEntities");
            DropForeignKey("dbo.AlbumEntities", "CoverPhotoId", "dbo.PhotoEntities");
            DropForeignKey("dbo.TagEntities", "PersonId", "dbo.PersonEntities");
            DropForeignKey("dbo.TagEntityPhotoEntities", "PhotoEntity_Id", "dbo.PhotoEntities");
            DropForeignKey("dbo.TagEntityPhotoEntities", "TagEntity_Id", "dbo.TagEntities");
            DropForeignKey("dbo.PhotoEntities", "ResolutionId", "dbo.ResolutionEntities");
            DropForeignKey("dbo.PhotoEntities", "AlbumId", "dbo.AlbumEntities");
            DropIndex("dbo.TagEntityPhotoEntities", new[] { "PhotoEntity_Id" });
            DropIndex("dbo.TagEntityPhotoEntities", new[] { "TagEntity_Id" });
            DropIndex("dbo.TagEntities", new[] { "PersonId" });
            DropIndex("dbo.PhotoEntities", new[] { "AlbumEntity_Id" });
            DropIndex("dbo.PhotoEntities", new[] { "AlbumId" });
            DropIndex("dbo.PhotoEntities", new[] { "ResolutionId" });
            DropIndex("dbo.AlbumEntities", new[] { "CoverPhotoId" });
            DropTable("dbo.TagEntityPhotoEntities");
            DropTable("dbo.PersonEntities");
            DropTable("dbo.TagEntities");
            DropTable("dbo.ResolutionEntities");
            DropTable("dbo.PhotoEntities");
            DropTable("dbo.AlbumEntities");
        }
    }
}
