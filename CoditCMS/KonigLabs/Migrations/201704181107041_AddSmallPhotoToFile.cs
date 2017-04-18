namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSmallPhotoToFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "IsMarked", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "IsMarked");
        }
    }
}
