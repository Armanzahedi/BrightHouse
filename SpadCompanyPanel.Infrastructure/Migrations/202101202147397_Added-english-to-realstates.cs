namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedenglishtorealstates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Options", "EnglishTitle", c => c.String(nullable: false, maxLength: 400));
            AddColumn("dbo.Plans", "EnglishTitle", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Plans", "EnglishDescription", c => c.String());
            AddColumn("dbo.RealStates", "EnglishTitle", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.RealStates", "EnglishShortDescription", c => c.String(maxLength: 1000));
            AddColumn("dbo.RealStates", "EnglishDescription", c => c.String());
            AddColumn("dbo.RealStates", "EnglishRegion", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RealStates", "EnglishRegion");
            DropColumn("dbo.RealStates", "EnglishDescription");
            DropColumn("dbo.RealStates", "EnglishShortDescription");
            DropColumn("dbo.RealStates", "EnglishTitle");
            DropColumn("dbo.Plans", "EnglishDescription");
            DropColumn("dbo.Plans", "EnglishTitle");
            DropColumn("dbo.Options", "EnglishTitle");
        }
    }
}
