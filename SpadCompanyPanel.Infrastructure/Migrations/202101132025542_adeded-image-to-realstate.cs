namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adededimagetorealstate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealStates", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RealStates", "Image");
        }
    }
}
