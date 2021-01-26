namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedrealStaterelatedtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        Unit = c.String(nullable: false, maxLength: 10),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        Title = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlanOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanId = c.Int(nullable: false),
                        OptionId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Options", t => t.OptionId, cascadeDelete: true)
                .ForeignKey("dbo.Plans", t => t.PlanId, cascadeDelete: true)
                .Index(t => t.PlanId)
                .Index(t => t.OptionId);
            
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Rooms = c.Int(nullable: false),
                        Area = c.Int(nullable: false),
                        BathRooms = c.Int(nullable: false),
                        FloorNo = c.Int(nullable: false),
                        Currency = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RentPrice = c.Decimal(precision: 18, scale: 2),
                        PlanType = c.Int(nullable: false),
                        RealStateId = c.Int(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        Video = c.String(),
                        VideoThumbnail = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RealStates", t => t.RealStateId, cascadeDelete: true)
                .Index(t => t.RealStateId);
            
            CreateTable(
                "dbo.RealStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Type = c.Int(nullable: false),
                        ShortDescription = c.String(maxLength: 1000),
                        Description = c.String(),
                        Region = c.String(maxLength: 500),
                        GeoDivisionId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LastViewDate = c.DateTime(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.GeoDivisions", t => t.GeoDivisionId, cascadeDelete: true)
                .Index(t => t.GeoDivisionId)
                .Index(t => t.CurrencyId);
            
            CreateTable(
                "dbo.RealStateComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        Email = c.String(nullable: false, maxLength: 400),
                        Message = c.String(nullable: false, maxLength: 800),
                        AddedDate = c.DateTime(),
                        ParentId = c.Int(),
                        ArticleId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.RealStateComments", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.RealStateGalleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(nullable: false, maxLength: 500),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RealStateComments", "ParentId", "dbo.RealStateComments");
            DropForeignKey("dbo.RealStateComments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.PlanOptions", "PlanId", "dbo.Plans");
            DropForeignKey("dbo.Plans", "RealStateId", "dbo.RealStates");
            DropForeignKey("dbo.RealStates", "GeoDivisionId", "dbo.GeoDivisions");
            DropForeignKey("dbo.RealStates", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.PlanOptions", "OptionId", "dbo.Options");
            DropIndex("dbo.RealStateComments", new[] { "ArticleId" });
            DropIndex("dbo.RealStateComments", new[] { "ParentId" });
            DropIndex("dbo.RealStates", new[] { "CurrencyId" });
            DropIndex("dbo.RealStates", new[] { "GeoDivisionId" });
            DropIndex("dbo.Plans", new[] { "RealStateId" });
            DropIndex("dbo.PlanOptions", new[] { "OptionId" });
            DropIndex("dbo.PlanOptions", new[] { "PlanId" });
            DropTable("dbo.RealStateGalleries");
            DropTable("dbo.RealStateComments");
            DropTable("dbo.RealStates");
            DropTable("dbo.Plans");
            DropTable("dbo.PlanOptions");
            DropTable("dbo.Options");
            DropTable("dbo.Currencies");
        }
    }
}
