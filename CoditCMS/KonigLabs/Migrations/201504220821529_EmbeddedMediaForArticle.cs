namespace KonigLabs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmbeddedMediaForArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "EmbeddedMedia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "EmbeddedMedia");
        }
    }
}
