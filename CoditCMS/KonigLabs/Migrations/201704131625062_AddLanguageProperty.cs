namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLanguageProperty : DbMigration
    {
        public override void Up()
        {
           /* AddColumn("dbo.ArticleCategories", "Language", c => c.String(maxLength: 2));
            AddColumn("dbo.Projects", "Language", c => c.String(maxLength: 2));
            AddColumn("dbo.Tags", "Language", c => c.String(maxLength: 2));
            CreateIndex("dbo.ArticleCategories", "Language", name: "Language");
            CreateIndex("dbo.Projects", "Language", name: "Language");
            CreateIndex("dbo.Tags", "Language", name: "Language");*/
        }
        
        public override void Down()
        {
          /*  DropIndex("dbo.Tags", "Language");
            DropIndex("dbo.Projects", "Language");
            DropIndex("dbo.ArticleCategories", "Language");
            DropColumn("dbo.Tags", "Language");
            DropColumn("dbo.Projects", "Language");
            DropColumn("dbo.ArticleCategories", "Language");*/
        }
    }
}
