namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogFields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleArticleCategories",
                c => new
                    {
                        Article_Id = c.Int(nullable: false),
                        ArticleCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_Id, t.ArticleCategory_Id })
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .ForeignKey("dbo.ArticleCategories", t => t.ArticleCategory_Id, cascadeDelete: true)
                .Index(t => t.Article_Id)
                .Index(t => t.ArticleCategory_Id);
            
            CreateTable(
                "dbo.TagArticles",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Article_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Article_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Article_Id);
            
            AddColumn("dbo.Articles", "ShowOnHomePage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.TagArticles", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.ArticleArticleCategories", "ArticleCategory_Id", "dbo.ArticleCategories");
            DropForeignKey("dbo.ArticleArticleCategories", "Article_Id", "dbo.Articles");
            DropIndex("dbo.TagArticles", new[] { "Article_Id" });
            DropIndex("dbo.TagArticles", new[] { "Tag_Id" });
            DropIndex("dbo.ArticleArticleCategories", new[] { "ArticleCategory_Id" });
            DropIndex("dbo.ArticleArticleCategories", new[] { "Article_Id" });
            DropColumn("dbo.Articles", "ShowOnHomePage");
            DropTable("dbo.TagArticles");
            DropTable("dbo.ArticleArticleCategories");
            DropTable("dbo.Tags");
            DropTable("dbo.ArticleCategories");
        }
    }
}
