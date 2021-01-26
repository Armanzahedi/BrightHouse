namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 400),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        ViewCount = c.Int(nullable: false),
                        Image = c.String(),
                        AddedDate = c.DateTime(),
                        ArticleCategoryId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleCategories", t => t.ArticleCategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ArticleCategoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ArticleComments",
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
                .ForeignKey("dbo.ArticleComments", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.ArticleHeadLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 700),
                        Description = c.String(),
                        ArticleId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.ArticleTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 300),
                        ArticleId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Avatar = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 300),
                        LastName = c.String(nullable: false, maxLength: 300),
                        Information = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Role_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.Role_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        RoleNameLocal = c.String(maxLength: 300),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 300),
                        Name = c.String(maxLength: 300),
                        DisplayPriority = c.Int(nullable: false),
                        ControllerName = c.String(maxLength: 300),
                        ActionName = c.String(maxLength: 300),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 600),
                        Email = c.String(nullable: false, maxLength: 600),
                        Message = c.String(nullable: false),
                        IsViewed = c.Boolean(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Faqs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(nullable: false),
                        Answer = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodGalleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        Food_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.Food_Id)
                .Index(t => t.Food_Id);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        FoodTypeId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodTypes", t => t.FoodTypeId)
                .Index(t => t.FoodTypeId);
            
            CreateTable(
                "dbo.FoodTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GalleryVideos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Video = c.String(),
                        Image = c.String(),
                        Title = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 200),
                        TableName = c.String(maxLength: 100),
                        EntityId = c.Int(nullable: false),
                        Action = c.String(maxLength: 100),
                        ActionDate = c.DateTime(nullable: false),
                        OldValue = c.String(),
                        NewValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OurTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        Image = c.String(),
                        Description = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceIncludes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 700),
                        Description = c.String(),
                        ServiceId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        FileInfo = c.String(),
                        File = c.String(),
                        Image = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StaticContentDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        Link = c.String(),
                        StaticContentTypeId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaticContentTypes", t => t.StaticContentTypeId, cascadeDelete: true)
                .Index(t => t.StaticContentTypeId);
            
            CreateTable(
                "dbo.StaticContentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 600),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        Image = c.String(),
                        Description = c.String(nullable: false),
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
            DropForeignKey("dbo.StaticContentDetails", "StaticContentTypeId", "dbo.StaticContentTypes");
            DropForeignKey("dbo.ServiceIncludes", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Foods", "FoodTypeId", "dbo.FoodTypes");
            DropForeignKey("dbo.FoodGalleries", "Food_Id", "dbo.Foods");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "Role_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.RolePermissions", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RolePermissions", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Permissions", "ParentId", "dbo.Permissions");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Articles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArticleTags", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleHeadLines", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleComments", "ParentId", "dbo.ArticleComments");
            DropForeignKey("dbo.ArticleComments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "ArticleCategoryId", "dbo.ArticleCategories");
            DropIndex("dbo.StaticContentDetails", new[] { "StaticContentTypeId" });
            DropIndex("dbo.ServiceIncludes", new[] { "ServiceId" });
            DropIndex("dbo.Foods", new[] { "FoodTypeId" });
            DropIndex("dbo.FoodGalleries", new[] { "Food_Id" });
            DropIndex("dbo.Permissions", new[] { "ParentId" });
            DropIndex("dbo.RolePermissions", new[] { "PermissionId" });
            DropIndex("dbo.RolePermissions", new[] { "RoleId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "Role_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ArticleTags", new[] { "ArticleId" });
            DropIndex("dbo.ArticleHeadLines", new[] { "ArticleId" });
            DropIndex("dbo.ArticleComments", new[] { "ArticleId" });
            DropIndex("dbo.ArticleComments", new[] { "ParentId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropIndex("dbo.Articles", new[] { "ArticleCategoryId" });
            DropTable("dbo.Testimonials");
            DropTable("dbo.StaticContentTypes");
            DropTable("dbo.StaticContentDetails");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceIncludes");
            DropTable("dbo.OurTeams");
            DropTable("dbo.Logs");
            DropTable("dbo.GalleryVideos");
            DropTable("dbo.Galleries");
            DropTable("dbo.FoodTypes");
            DropTable("dbo.Foods");
            DropTable("dbo.FoodGalleries");
            DropTable("dbo.Faqs");
            DropTable("dbo.ContactForms");
            DropTable("dbo.Certificates");
            DropTable("dbo.Permissions");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ArticleTags");
            DropTable("dbo.ArticleHeadLines");
            DropTable("dbo.ArticleComments");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleCategories");
        }
    }
}
