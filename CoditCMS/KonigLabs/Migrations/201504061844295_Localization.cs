namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using KonigLabs.Models;
    public partial class Localization : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Language", c => c.String(maxLength: 2, defaultValue: LocalEntity.RU, nullable: false));
            AddColumn("dbo.Clients", "Language", c => c.String(maxLength: 2, defaultValue: LocalEntity.RU, nullable: false));
            AddColumn("dbo.CrewMembers", "Language", c => c.String(maxLength: 2, defaultValue: LocalEntity.RU, nullable: false));
            AddColumn("dbo.ProjectCategories", "Language", c => c.String(maxLength: 2, defaultValue: LocalEntity.RU, nullable: false));
            CreateIndex("dbo.Articles", "Language", name: "Language");
            CreateIndex("dbo.Clients", "Language", name: "Language");
            CreateIndex("dbo.CrewMembers", "Language", name: "Language");
            CreateIndex("dbo.ProjectCategories", "Language", name: "Language");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProjectCategories", "Language");
            DropIndex("dbo.CrewMembers", "Language");
            DropIndex("dbo.Clients", "Language");
            DropIndex("dbo.Articles", "Language");
            DropColumn("dbo.ProjectCategories", "Language");
            DropColumn("dbo.CrewMembers", "Language");
            DropColumn("dbo.Clients", "Language");
            DropColumn("dbo.Articles", "Language");
        }
    }
}
