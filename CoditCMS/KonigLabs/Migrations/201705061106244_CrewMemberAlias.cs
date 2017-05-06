namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrewMemberAlias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CrewMembers", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CrewMembers", "Alias");
        }
    }
}
