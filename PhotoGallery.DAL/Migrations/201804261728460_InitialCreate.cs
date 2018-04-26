namespace PhotoGallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                    })
                .PrimaryKey(t => t.Id);
            
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlbumEntities", t => t.AlbumId)
                .ForeignKey("dbo.ResolutionEntities", t => t.ResolutionId)
                .Index(t => t.ResolutionId)
                .Index(t => t.AlbumId);
            
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
                        ItemId = c.Int(),
                        PersonId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        PhotoEntity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemEntities", t => t.ItemId)
                .ForeignKey("dbo.PersonEntities", t => t.PersonId)
                .ForeignKey("dbo.PhotoEntities", t => t.PhotoEntity_Id, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.PersonId)
                .Index(t => t.PhotoEntity_Id);
            
            CreateTable(
                "dbo.ItemEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagEntities", "PhotoEntity_Id", "dbo.PhotoEntities");
            DropForeignKey("dbo.TagEntities", "PersonId", "dbo.PersonEntities");
            DropForeignKey("dbo.TagEntities", "ItemId", "dbo.ItemEntities");
            DropForeignKey("dbo.PhotoEntities", "ResolutionId", "dbo.ResolutionEntities");
            DropForeignKey("dbo.PhotoEntities", "AlbumId", "dbo.AlbumEntities");
            DropIndex("dbo.TagEntities", new[] { "PhotoEntity_Id" });
            DropIndex("dbo.TagEntities", new[] { "PersonId" });
            DropIndex("dbo.TagEntities", new[] { "ItemId" });
            DropIndex("dbo.PhotoEntities", new[] { "AlbumId" });
            DropIndex("dbo.PhotoEntities", new[] { "ResolutionId" });
            DropTable("dbo.PersonEntities");
            DropTable("dbo.ItemEntities");
            DropTable("dbo.TagEntities");
            DropTable("dbo.ResolutionEntities");
            DropTable("dbo.PhotoEntities");
            DropTable("dbo.AlbumEntities");
        }
    }
}
