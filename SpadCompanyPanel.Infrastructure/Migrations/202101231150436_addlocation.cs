namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealStates", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RealStates", "Location");
        }
    }
}
