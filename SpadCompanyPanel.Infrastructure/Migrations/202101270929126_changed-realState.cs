namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedrealState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealStates", "VideoStr", c => c.String());
            DropColumn("dbo.RealStates", "Video");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RealStates", "Video", c => c.String());
            DropColumn("dbo.RealStates", "VideoStr");
        }
    }
}
