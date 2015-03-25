namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Galleries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SourceName = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Size = c.Long(),
                        Alt = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Alias = c.String(),
                        Description = c.String(),
                        Sort = c.Int(nullable: false),
                        Visibility = c.Boolean(nullable: false),
                        MetaTitle = c.String(),
                        MetaKeywords = c.String(),
                        MetaDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GalleryFiles",
                c => new
                    {
                        Gallery_Id = c.Int(nullable: false),
                        File_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Gallery_Id, t.File_Id })
                .ForeignKey("dbo.Galleries", t => t.Gallery_Id, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.File_Id, cascadeDelete: true)
                .Index(t => t.Gallery_Id)
                .Index(t => t.File_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GalleryFiles", "File_Id", "dbo.Files");
            DropForeignKey("dbo.GalleryFiles", "Gallery_Id", "dbo.Galleries");
            DropIndex("dbo.GalleryFiles", new[] { "File_Id" });
            DropIndex("dbo.GalleryFiles", new[] { "Gallery_Id" });
            DropTable("dbo.GalleryFiles");
            DropTable("dbo.Galleries");
            DropTable("dbo.Files");
        }
    }
}
