namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuthorSignature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CrewMembers", "Signature", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CrewMembers", "Signature");
        }
    }
}
