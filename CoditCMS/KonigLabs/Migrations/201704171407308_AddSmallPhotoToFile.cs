namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSmallPhotoToFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "IsSmallPhoto", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "IsSmallPhoto");
        }
    }
}
