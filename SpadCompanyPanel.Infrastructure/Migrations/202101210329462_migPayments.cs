namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migPayments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NationalCode = c.String(maxLength: 100),
                        Address = c.String(),
                        PostalCode = c.String(maxLength: 100),
                        UserId = c.String(maxLength: 128),
                        GeoDivisionId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeoDivisions", t => t.GeoDivisionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.GeoDivisionId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalPrice = c.Long(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        InvoiceNumber = c.String(),
                        CustomerId = c.Int(),
                        CustomerName = c.String(maxLength: 500),
                        GeoDivisionId = c.Int(),
                        Address = c.String(maxLength: 500),
                        Phone = c.String(maxLength: 50),
                        PostalCode = c.String(maxLength: 50),
                        Email = c.String(),
                        RealStateId = c.Int(),
                        PlanId = c.Int(),
                        Description = c.String(),
                        IsPayed = c.Boolean(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.GeoDivisions", t => t.GeoDivisionId)
                .ForeignKey("dbo.Plans", t => t.PlanId)
                .ForeignKey("dbo.RealStates", t => t.RealStateId)
                .Index(t => t.CurrencyId)
                .Index(t => t.CustomerId)
                .Index(t => t.GeoDivisionId)
                .Index(t => t.RealStateId)
                .Index(t => t.PlanId);
            
            CreateTable(
                "dbo.EPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        Amount = c.Long(nullable: false),
                        Description = c.String(maxLength: 2000),
                        ExtraInfo = c.String(maxLength: 255),
                        RetrievalRefNo = c.String(),
                        SystemTraceNo = c.String(),
                        Token = c.String(),
                        Url = c.String(),
                        PaymentKey = c.String(),
                        Title = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invoices", "RealStateId", "dbo.RealStates");
            DropForeignKey("dbo.Invoices", "PlanId", "dbo.Plans");
            DropForeignKey("dbo.Invoices", "GeoDivisionId", "dbo.GeoDivisions");
            DropForeignKey("dbo.EPayments", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Invoices", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Customers", "GeoDivisionId", "dbo.GeoDivisions");
            DropIndex("dbo.EPayments", new[] { "InvoiceId" });
            DropIndex("dbo.Invoices", new[] { "PlanId" });
            DropIndex("dbo.Invoices", new[] { "RealStateId" });
            DropIndex("dbo.Invoices", new[] { "GeoDivisionId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            DropIndex("dbo.Invoices", new[] { "CurrencyId" });
            DropIndex("dbo.Customers", new[] { "GeoDivisionId" });
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropTable("dbo.EPayments");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
        }
    }
}
