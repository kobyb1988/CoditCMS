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
                        Gallery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Galleries", t => t.Gallery_Id)
                .Index(t => t.Gallery_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "Gallery_Id", "dbo.Galleries");
            DropIndex("dbo.Files", new[] { "Gallery_Id" });
            DropTable("dbo.Galleries");
            DropTable("dbo.Files");
        }
    }
}
