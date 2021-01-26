namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednewsandnewsletter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        EnglishTitle = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        EnglishShortDescription = c.String(),
                        Description = c.String(),
                        EnglishDescription = c.String(),
                        ViewCount = c.Int(nullable: false),
                        Image = c.String(),
                        AddedDate = c.DateTime(),
                        UserId = c.String(maxLength: 128),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NewsLetterMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
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
            DropForeignKey("dbo.News", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.News", new[] { "UserId" });
            DropTable("dbo.NewsLetterMembers");
            DropTable("dbo.News");
        }
    }
}
