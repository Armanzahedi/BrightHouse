namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedinvoices : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EPayments", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "GeoDivisionId", "dbo.GeoDivisions");
            DropForeignKey("dbo.Invoices", "RealStateId", "dbo.RealStates");
            DropIndex("dbo.Invoices", new[] { "GeoDivisionId" });
            DropIndex("dbo.Invoices", new[] { "RealStateId" });
            DropIndex("dbo.EPayments", new[] { "InvoiceId" });
            CreateTable(
                "dbo.PaymentAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        QrCode = c.String(),
                        Address = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Invoices", "PaymentAccountId", c => c.Int());
            AddColumn("dbo.Invoices", "RegisteredDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Invoices", "PaymentDate", c => c.DateTime());
            AddColumn("dbo.Invoices", "PaymentConfirmDate", c => c.DateTime());
            AddColumn("dbo.Invoices", "PaymentStatus", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoices", "PaymentAccountId");
            AddForeignKey("dbo.Invoices", "PaymentAccountId", "dbo.PaymentAccounts", "Id");
            DropColumn("dbo.Invoices", "AddedDate");
            DropColumn("dbo.Invoices", "CustomerName");
            DropColumn("dbo.Invoices", "GeoDivisionId");
            DropColumn("dbo.Invoices", "Address");
            DropColumn("dbo.Invoices", "Phone");
            DropColumn("dbo.Invoices", "PostalCode");
            DropColumn("dbo.Invoices", "Email");
            DropColumn("dbo.Invoices", "RealStateId");
            DropColumn("dbo.Invoices", "IsPayed");
            DropTable("dbo.EPayments");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Invoices", "IsPayed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Invoices", "RealStateId", c => c.Int());
            AddColumn("dbo.Invoices", "Email", c => c.String());
            AddColumn("dbo.Invoices", "PostalCode", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "Phone", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "Address", c => c.String(maxLength: 500));
            AddColumn("dbo.Invoices", "GeoDivisionId", c => c.Int());
            AddColumn("dbo.Invoices", "CustomerName", c => c.String(maxLength: 500));
            AddColumn("dbo.Invoices", "AddedDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Invoices", "PaymentAccountId", "dbo.PaymentAccounts");
            DropIndex("dbo.Invoices", new[] { "PaymentAccountId" });
            DropColumn("dbo.Invoices", "PaymentStatus");
            DropColumn("dbo.Invoices", "PaymentConfirmDate");
            DropColumn("dbo.Invoices", "PaymentDate");
            DropColumn("dbo.Invoices", "RegisteredDate");
            DropColumn("dbo.Invoices", "PaymentAccountId");
            DropTable("dbo.PaymentAccounts");
            CreateIndex("dbo.EPayments", "InvoiceId");
            CreateIndex("dbo.Invoices", "RealStateId");
            CreateIndex("dbo.Invoices", "GeoDivisionId");
            AddForeignKey("dbo.Invoices", "RealStateId", "dbo.RealStates", "Id");
            AddForeignKey("dbo.Invoices", "GeoDivisionId", "dbo.GeoDivisions", "Id");
            AddForeignKey("dbo.EPayments", "InvoiceId", "dbo.Invoices", "Id", cascadeDelete: true);
        }
    }
}
