namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedmigrationtoarticlecategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleCategories", "EnglishTitle", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleCategories", "EnglishTitle");
        }
    }
}
