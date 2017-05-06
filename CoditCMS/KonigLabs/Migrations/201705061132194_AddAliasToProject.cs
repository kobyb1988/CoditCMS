namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAliasToProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Alias");
        }
    }
}
