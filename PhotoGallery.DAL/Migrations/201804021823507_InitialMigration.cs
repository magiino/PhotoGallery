namespace PhotoGallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CoverPhotoId = c.Int(),
                        PhotoEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhotoEntities", t => t.PhotoEntity_Id)
                .ForeignKey("dbo.PhotoEntities", t => t.CoverPhotoId)
                .Index(t => t.CoverPhotoId)
                .Index(t => t.PhotoEntity_Id);
            
            CreateTable(
                "dbo.PhotoEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        Format = c.Int(nullable: false),
                        ResolutionId = c.Int(nullable: false),
                        Note = c.String(),
                        Location = c.String(),
                        AlbumEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResolutionEntities", t => t.ResolutionId, cascadeDelete: true)
                .ForeignKey("dbo.AlbumEntities", t => t.AlbumEntity_Id)
                .Index(t => t.ResolutionId)
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
                        PositionOnPhotoX = c.Int(nullable: false),
                        PositionOnPhotoY = c.Int(nullable: false),
                        Name = c.String(),
                        PersonId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonEntities", t => t.PersonId, cascadeDelete: true)
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
                .ForeignKey("dbo.TagEntities", t => t.TagEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.PhotoEntities", t => t.PhotoEntity_Id, cascadeDelete: true)
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
            DropForeignKey("dbo.AlbumEntities", "PhotoEntity_Id", "dbo.PhotoEntities");
            DropIndex("dbo.TagEntityPhotoEntities", new[] { "PhotoEntity_Id" });
            DropIndex("dbo.TagEntityPhotoEntities", new[] { "TagEntity_Id" });
            DropIndex("dbo.TagEntities", new[] { "PersonId" });
            DropIndex("dbo.PhotoEntities", new[] { "AlbumEntity_Id" });
            DropIndex("dbo.PhotoEntities", new[] { "ResolutionId" });
            DropIndex("dbo.AlbumEntities", new[] { "PhotoEntity_Id" });
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
