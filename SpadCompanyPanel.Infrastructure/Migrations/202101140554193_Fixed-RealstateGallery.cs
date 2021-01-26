namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedRealstateGallery : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RealStateGalleries", "RealStateId", c => c.Int());
            CreateIndex("dbo.RealStateGalleries", "RealStateId");
            AddForeignKey("dbo.RealStateGalleries", "RealStateId", "dbo.RealStates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RealStateGalleries", "RealStateId", "dbo.RealStates");
            DropIndex("dbo.RealStateGalleries", new[] { "RealStateId" });
            DropColumn("dbo.RealStateGalleries", "RealStateId");
        }
    }
}
