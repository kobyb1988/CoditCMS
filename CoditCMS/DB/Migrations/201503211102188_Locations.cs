namespace DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Locations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(),
                        Alias = c.String(),
                        Teaser = c.String(),
                        Content = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        MetaTitle = c.String(),
                        MetaKeywords = c.String(),
                        MetaDescription = c.String(),
                        ShowInMenu = c.Boolean(nullable: false),
                        Level = c.Int(nullable: false),
                        HasChilds = c.Boolean(nullable: false),
                        Location_Id = c.Int(),
                        Location_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id1)
                .ForeignKey("dbo.Locations", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.Location_Id)
                .Index(t => t.Location_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "ParentId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "Location_Id1", "dbo.Locations");
            DropForeignKey("dbo.Locations", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Locations", new[] { "Location_Id1" });
            DropIndex("dbo.Locations", new[] { "Location_Id" });
            DropIndex("dbo.Locations", new[] { "ParentId" });
            DropTable("dbo.Locations");
        }
    }
}
