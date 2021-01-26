namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedrealstatecomment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RealStateComments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.RealStateComments", new[] { "ArticleId" });
            AddColumn("dbo.RealStateComments", "RealSteteId", c => c.Int(nullable: false));
            AddColumn("dbo.RealStateComments", "RealState_Id", c => c.Int());
            CreateIndex("dbo.RealStateComments", "RealState_Id");
            AddForeignKey("dbo.RealStateComments", "RealState_Id", "dbo.RealStates", "Id");
            DropColumn("dbo.RealStateComments", "ArticleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RealStateComments", "ArticleId", c => c.Int(nullable: false));
            DropForeignKey("dbo.RealStateComments", "RealState_Id", "dbo.RealStates");
            DropIndex("dbo.RealStateComments", new[] { "RealState_Id" });
            DropColumn("dbo.RealStateComments", "RealState_Id");
            DropColumn("dbo.RealStateComments", "RealSteteId");
            CreateIndex("dbo.RealStateComments", "ArticleId");
            AddForeignKey("dbo.RealStateComments", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
        }
    }
}
