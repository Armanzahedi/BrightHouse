using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using SpadCompanyPanel.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SpadCompanyPanel.Infrastructure
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleHeadLine> ArticleHeadLines { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<StaticContentType> StaticContentTypes { get; set; }
        public DbSet<StaticContentDetail> StaticContentDetails { get; set; }
        public DbSet<OurTeam> OurTeams { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceInclude> ServiceIncludes { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<FoodGallery> FoodGalleries { get; set; }
        public DbSet<GalleryVideo> GalleryVideos { get; set; }
        public DbSet<GeoDivision> GeoDivisions { get; set; }
        public DbSet<RealState> RealStates { get; set; }
        public DbSet<RealStateComment> RealStateComments { get; set; }
        public DbSet<RealStateGallery> RealStateGalleries { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanOption> PlanOptions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<EPayment> ePayments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsLetterMember> NewsLetterMembers { get; set; }
    }
}
