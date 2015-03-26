namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clients : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FileCrewMembers", newName: "CrewMemberFiles");
            DropPrimaryKey("dbo.CrewMemberFiles");
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        MetaTitle = c.String(),
                        MetaDescription = c.String(),
                        MetaKeywords = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileClients",
                c => new
                    {
                        File_Id = c.Int(nullable: false),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.File_Id, t.Client_Id })
                .ForeignKey("dbo.Files", t => t.File_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.File_Id)
                .Index(t => t.Client_Id);
            
            AddPrimaryKey("dbo.CrewMemberFiles", new[] { "CrewMember_Id", "File_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileClients", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.FileClients", "File_Id", "dbo.Files");
            DropIndex("dbo.FileClients", new[] { "Client_Id" });
            DropIndex("dbo.FileClients", new[] { "File_Id" });
            DropPrimaryKey("dbo.CrewMemberFiles");
            DropTable("dbo.FileClients");
            DropTable("dbo.Clients");
            AddPrimaryKey("dbo.CrewMemberFiles", new[] { "File_Id", "CrewMember_Id" });
            RenameTable(name: "dbo.CrewMemberFiles", newName: "FileCrewMembers");
        }
    }
}
