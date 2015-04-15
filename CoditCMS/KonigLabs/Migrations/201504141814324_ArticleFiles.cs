namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleFiles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "Article_Id", "dbo.Articles");
            DropIndex("dbo.Files", new[] { "Article_Id" });
            CreateTable(
                "dbo.FileArticles",
                c => new
                    {
                        File_Id = c.Int(nullable: false),
                        Article_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.File_Id, t.Article_Id })
                .ForeignKey("dbo.Files", t => t.File_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.File_Id)
                .Index(t => t.Article_Id);
            
            DropColumn("dbo.Files", "Article_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Article_Id", c => c.Int());
            DropForeignKey("dbo.FileArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.FileArticles", "File_Id", "dbo.Files");
            DropIndex("dbo.FileArticles", new[] { "Article_Id" });
            DropIndex("dbo.FileArticles", new[] { "File_Id" });
            DropTable("dbo.FileArticles");
            CreateIndex("dbo.Files", "Article_Id");
            AddForeignKey("dbo.Files", "Article_Id", "dbo.Articles", "Id");
        }
    }
}
