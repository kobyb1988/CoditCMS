namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrewMember : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrewMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Title = c.String(),
                        Bio = c.String(),
                        MetaTitle = c.String(),
                        MetaDescription = c.String(),
                        MetaKeywords = c.String(),
                        Visibility = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CrewMembers");
        }
    }
}
