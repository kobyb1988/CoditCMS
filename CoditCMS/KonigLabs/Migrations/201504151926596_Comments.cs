namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        ArticleId = c.Int(nullable: false),
                        CrewMemberId = c.Int(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentId)
                .ForeignKey("dbo.CrewMembers", t => t.CrewMemberId)
                .Index(t => t.ArticleId)
                .Index(t => t.CrewMemberId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CrewMemberId", "dbo.CrewMembers");
            DropForeignKey("dbo.Comments", "ParentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "ParentId" });
            DropIndex("dbo.Comments", new[] { "CrewMemberId" });
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropTable("dbo.Comments");
        }
    }
}
