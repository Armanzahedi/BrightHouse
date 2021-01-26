namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedplanintfieldstostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Plans", "Rooms", c => c.String(nullable: false));
            AlterColumn("dbo.Plans", "Area", c => c.String(nullable: false));
            AlterColumn("dbo.Plans", "BathRooms", c => c.String(nullable: false));
            AlterColumn("dbo.Plans", "FloorNo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Plans", "FloorNo", c => c.Int(nullable: false));
            AlterColumn("dbo.Plans", "BathRooms", c => c.Int(nullable: false));
            AlterColumn("dbo.Plans", "Area", c => c.Int(nullable: false));
            AlterColumn("dbo.Plans", "Rooms", c => c.Int(nullable: false));
        }
    }
}
