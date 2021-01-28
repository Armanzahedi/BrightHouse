namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedgeofromcustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "GeoDivisionId", "dbo.GeoDivisions");
            DropIndex("dbo.Customers", new[] { "GeoDivisionId" });
            DropColumn("dbo.Customers", "GeoDivisionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "GeoDivisionId", c => c.Int());
            CreateIndex("dbo.Customers", "GeoDivisionId");
            AddForeignKey("dbo.Customers", "GeoDivisionId", "dbo.GeoDivisions", "Id");
        }
    }
}
