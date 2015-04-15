namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Authors : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CrewMemberFiles", newName: "FileCrewMembers");
            DropPrimaryKey("dbo.FileCrewMembers");
            AddColumn("dbo.Articles", "CrewMemberId", c => c.Int());
            AddPrimaryKey("dbo.FileCrewMembers", new[] { "File_Id", "CrewMember_Id" });
            CreateIndex("dbo.Articles", "CrewMemberId");
            AddForeignKey("dbo.Articles", "CrewMemberId", "dbo.CrewMembers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "CrewMemberId", "dbo.CrewMembers");
            DropIndex("dbo.Articles", new[] { "CrewMemberId" });
            DropPrimaryKey("dbo.FileCrewMembers");
            DropColumn("dbo.Articles", "CrewMemberId");
            AddPrimaryKey("dbo.FileCrewMembers", new[] { "CrewMember_Id", "File_Id" });
            RenameTable(name: "dbo.FileCrewMembers", newName: "CrewMemberFiles");
        }
    }
}
