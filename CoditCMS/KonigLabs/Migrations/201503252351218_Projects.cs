namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Projects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        MetaTitle = c.String(),
                        MetaDescription = c.String(),
                        MetaKeywords = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        ProjectCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectCategories", t => t.ProjectCategoryID, cascadeDelete: true)
                .Index(t => t.ProjectCategoryID);
            
            CreateTable(
                "dbo.ProjectCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        MetaTitle = c.String(),
                        MetaDescription = c.String(),
                        MetaKeywords = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        ProjectCategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectFiles",
                c => new
                    {
                        Project_Id = c.Int(nullable: false),
                        File_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_Id, t.File_Id })
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.File_Id, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => t.File_Id);
            
            CreateTable(
                "dbo.ProjectCategoryProjectCategories",
                c => new
                    {
                        ProjectCategory_Id = c.Int(nullable: false),
                        ProjectCategory_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectCategory_Id, t.ProjectCategory_Id1 })
                .ForeignKey("dbo.ProjectCategories", t => t.ProjectCategory_Id)
                .ForeignKey("dbo.ProjectCategories", t => t.ProjectCategory_Id1)
                .Index(t => t.ProjectCategory_Id)
                .Index(t => t.ProjectCategory_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ProjectCategoryID", "dbo.ProjectCategories");
            DropForeignKey("dbo.ProjectCategoryProjectCategories", "ProjectCategory_Id1", "dbo.ProjectCategories");
            DropForeignKey("dbo.ProjectCategoryProjectCategories", "ProjectCategory_Id", "dbo.ProjectCategories");
            DropForeignKey("dbo.ProjectFiles", "File_Id", "dbo.Files");
            DropForeignKey("dbo.ProjectFiles", "Project_Id", "dbo.Projects");
            DropIndex("dbo.ProjectCategoryProjectCategories", new[] { "ProjectCategory_Id1" });
            DropIndex("dbo.ProjectCategoryProjectCategories", new[] { "ProjectCategory_Id" });
            DropIndex("dbo.ProjectFiles", new[] { "File_Id" });
            DropIndex("dbo.ProjectFiles", new[] { "Project_Id" });
            DropIndex("dbo.Projects", new[] { "ProjectCategoryID" });
            DropTable("dbo.ProjectCategoryProjectCategories");
            DropTable("dbo.ProjectFiles");
            DropTable("dbo.ProjectCategories");
            DropTable("dbo.Projects");
        }
    }
}
