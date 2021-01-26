namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RealStates", "Rate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RealStates", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
