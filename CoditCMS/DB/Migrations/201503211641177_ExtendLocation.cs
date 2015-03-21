namespace DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "Header", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "Header");
        }
    }
}
