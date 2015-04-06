namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Localisation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Language", c => c.String(maxLength: 2));
            AddColumn("dbo.Clients", "Language", c => c.String(maxLength: 2));
            AddColumn("dbo.CrewMembers", "Language", c => c.String(maxLength: 2));
            AddColumn("dbo.ProjectCategories", "Language", c => c.String(maxLength: 2));
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
