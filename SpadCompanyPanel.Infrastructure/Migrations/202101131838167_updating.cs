namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeoDivisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 200),
                        LatinTitle = c.String(maxLength: 200),
                        GeoDivisionType = c.Int(nullable: false),
                        IsLeaf = c.Int(),
                        IsArchived = c.Int(),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeoDivisions", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GeoDivisions", "ParentId", "dbo.GeoDivisions");
            DropIndex("dbo.GeoDivisions", new[] { "ParentId" });
            DropTable("dbo.GeoDivisions");
        }
    }
}
