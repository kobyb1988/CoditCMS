namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SourceName = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Size = c.Long(),
                        Alt = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileCrewMembers",
                c => new
                    {
                        File_Id = c.Int(nullable: false),
                        CrewMember_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.File_Id, t.CrewMember_Id })
                .ForeignKey("dbo.Files", t => t.File_Id, cascadeDelete: true)
                .ForeignKey("dbo.CrewMembers", t => t.CrewMember_Id, cascadeDelete: true)
                .Index(t => t.File_Id)
                .Index(t => t.CrewMember_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileCrewMembers", "CrewMember_Id", "dbo.CrewMembers");
            DropForeignKey("dbo.FileCrewMembers", "File_Id", "dbo.Files");
            DropIndex("dbo.FileCrewMembers", new[] { "CrewMember_Id" });
            DropIndex("dbo.FileCrewMembers", new[] { "File_Id" });
            DropTable("dbo.FileCrewMembers");
            DropTable("dbo.Files");
        }
    }
}
