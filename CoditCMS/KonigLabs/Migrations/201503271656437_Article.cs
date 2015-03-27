namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Article : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FileClients", newName: "ClientFiles");
            DropPrimaryKey("dbo.ClientFiles");
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                        MetaTitle = c.String(),
                        MetaDescription = c.String(),
                        MetaKeywords = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Files", "Article_Id", c => c.Int());
            AddPrimaryKey("dbo.ClientFiles", new[] { "Client_Id", "File_Id" });
            CreateIndex("dbo.Files", "Article_Id");
            AddForeignKey("dbo.Files", "Article_Id", "dbo.Articles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "Article_Id", "dbo.Articles");
            DropIndex("dbo.Files", new[] { "Article_Id" });
            DropPrimaryKey("dbo.ClientFiles");
            DropColumn("dbo.Files", "Article_Id");
            DropTable("dbo.Articles");
            AddPrimaryKey("dbo.ClientFiles", new[] { "File_Id", "Client_Id" });
            RenameTable(name: "dbo.ClientFiles", newName: "FileClients");
        }
    }
}
