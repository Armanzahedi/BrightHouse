namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedrealstatecomment2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RealStateComments", "RealState_Id", "dbo.RealStates");
            DropIndex("dbo.RealStateComments", new[] { "RealState_Id" });
            RenameColumn(table: "dbo.RealStateComments", name: "RealState_Id", newName: "RealStateId");
            AlterColumn("dbo.RealStateComments", "RealStateId", c => c.Int(nullable: false));
            CreateIndex("dbo.RealStateComments", "RealStateId");
            AddForeignKey("dbo.RealStateComments", "RealStateId", "dbo.RealStates", "Id", cascadeDelete: true);
            DropColumn("dbo.RealStateComments", "RealSteteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RealStateComments", "RealSteteId", c => c.Int(nullable: false));
            DropForeignKey("dbo.RealStateComments", "RealStateId", "dbo.RealStates");
            DropIndex("dbo.RealStateComments", new[] { "RealStateId" });
            AlterColumn("dbo.RealStateComments", "RealStateId", c => c.Int());
            RenameColumn(table: "dbo.RealStateComments", name: "RealStateId", newName: "RealState_Id");
            CreateIndex("dbo.RealStateComments", "RealState_Id");
            AddForeignKey("dbo.RealStateComments", "RealState_Id", "dbo.RealStates", "Id");
        }
    }
}
