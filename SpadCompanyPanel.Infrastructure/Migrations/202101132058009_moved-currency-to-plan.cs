namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movedcurrencytoplan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RealStates", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.RealStates", new[] { "CurrencyId" });
            AddColumn("dbo.Plans", "CurrencyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Plans", "CurrencyId");
            AddForeignKey("dbo.Plans", "CurrencyId", "dbo.Currencies", "Id", cascadeDelete: true);
            DropColumn("dbo.Plans", "Currency");
            DropColumn("dbo.RealStates", "CurrencyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RealStates", "CurrencyId", c => c.Int(nullable: false));
            AddColumn("dbo.Plans", "Currency", c => c.Int(nullable: false));
            DropForeignKey("dbo.Plans", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.Plans", new[] { "CurrencyId" });
            DropColumn("dbo.Plans", "CurrencyId");
            CreateIndex("dbo.RealStates", "CurrencyId");
            AddForeignKey("dbo.RealStates", "CurrencyId", "dbo.Currencies", "Id", cascadeDelete: true);
        }
    }
}
