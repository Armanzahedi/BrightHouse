namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfloatnumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Currencies", "ExchangeRate", c => c.Single(nullable: false));
            AlterColumn("dbo.Plans", "Price", c => c.Single(nullable: false));
            AlterColumn("dbo.Plans", "RentPrice", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Plans", "RentPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Plans", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Currencies", "ExchangeRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
