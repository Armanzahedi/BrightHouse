namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedmigrationtoarticles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "EnglishTitle", c => c.String(nullable: false, maxLength: 600));
            AddColumn("dbo.Articles", "EnglishShortDescription", c => c.String());
            AddColumn("dbo.Articles", "EnglishDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "EnglishDescription");
            DropColumn("dbo.Articles", "EnglishShortDescription");
            DropColumn("dbo.Articles", "EnglishTitle");
        }
    }
}
