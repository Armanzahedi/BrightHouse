namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedvideotorealstate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealStates", "VideoThumbnail", c => c.String());
            AddColumn("dbo.RealStates", "Video", c => c.String());
            DropColumn("dbo.Plans", "Video");
            DropColumn("dbo.Plans", "VideoThumbnail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plans", "VideoThumbnail", c => c.String());
            AddColumn("dbo.Plans", "Video", c => c.String());
            DropColumn("dbo.RealStates", "Video");
            DropColumn("dbo.RealStates", "VideoThumbnail");
        }
    }
}
