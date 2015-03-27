namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectCategoryProjectCategories", "ProjectCategory_Id", "dbo.ProjectCategories");
            DropForeignKey("dbo.ProjectCategoryProjectCategories", "ProjectCategory_Id1", "dbo.ProjectCategories");
            DropForeignKey("dbo.Projects", "ProjectCategoryID", "dbo.ProjectCategories");
            DropIndex("dbo.Projects", new[] { "ProjectCategoryID" });
            DropIndex("dbo.ProjectCategoryProjectCategories", new[] { "ProjectCategory_Id" });
            DropIndex("dbo.ProjectCategoryProjectCategories", new[] { "ProjectCategory_Id1" });
            CreateTable(
                "dbo.ProjectCategoryProjects",
                c => new
                    {
                        ProjectCategory_Id = c.Int(nullable: false),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectCategory_Id, t.Project_Id })
                .ForeignKey("dbo.ProjectCategories", t => t.ProjectCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.ProjectCategory_Id)
                .Index(t => t.Project_Id);
            
            DropColumn("dbo.Projects", "ProjectCategoryID");
            DropColumn("dbo.ProjectCategories", "ProjectCategoryID");
            DropTable("dbo.ProjectCategoryProjectCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProjectCategoryProjectCategories",
                c => new
                    {
                        ProjectCategory_Id = c.Int(nullable: false),
                        ProjectCategory_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectCategory_Id, t.ProjectCategory_Id1 });
            
            AddColumn("dbo.ProjectCategories", "ProjectCategoryID", c => c.Int());
            AddColumn("dbo.Projects", "ProjectCategoryID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProjectCategoryProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ProjectCategoryProjects", "ProjectCategory_Id", "dbo.ProjectCategories");
            DropIndex("dbo.ProjectCategoryProjects", new[] { "Project_Id" });
            DropIndex("dbo.ProjectCategoryProjects", new[] { "ProjectCategory_Id" });
            DropTable("dbo.ProjectCategoryProjects");
            CreateIndex("dbo.ProjectCategoryProjectCategories", "ProjectCategory_Id1");
            CreateIndex("dbo.ProjectCategoryProjectCategories", "ProjectCategory_Id");
            CreateIndex("dbo.Projects", "ProjectCategoryID");
            AddForeignKey("dbo.Projects", "ProjectCategoryID", "dbo.ProjectCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectCategoryProjectCategories", "ProjectCategory_Id1", "dbo.ProjectCategories", "Id");
            AddForeignKey("dbo.ProjectCategoryProjectCategories", "ProjectCategory_Id", "dbo.ProjectCategories", "Id");
        }
    }
}
