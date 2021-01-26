//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Visa.Core.Models;

//namespace Visa.Infrastructure
//{
//    public static class ModelBuilderExtensions
//    {
//        public static void Seed(this ModelBuilder modelBuilder)
//        {
//            var ADMIN_ID = "75625814-138e-4831-a1ea-bf58e211adff";
//            var ADMIN_ROLE_ID = "29bd76db-5835-406d-ad1d-7a0901447c18";
//            var MANAGER_ROLE_ID = "d7be43da-622c-4cfe-98a9-5a5161120d24";
//            var AUTHOR_ROLE_ID = "d7be43da-622c-4cfe-98a9-5a5161120d25";
//            var admin = new User()
//            {
//                Id = ADMIN_ID,
//                FirstName = "Admin",
//                LastName = "Admin",
//                UserName = "Admin",
//                NormalizedUserName = "ADMIN",
//                Email = "Admin@Admin.com",
//                NormalizedEmail = "ADMIN@ADMIN.COM"
//            };
//            admin.PasswordHash = GetHashedPassword(admin, "Admin");

//            modelBuilder.Entity<User>().HasData(
//                admin
//            );
//            modelBuilder.Entity<IdentityRole>().HasData(
//                new IdentityRole { Id = ADMIN_ROLE_ID, Name = "Admin", NormalizedName = "ADMIN" },
//                new IdentityRole { Id = MANAGER_ROLE_ID, Name = "Manager", NormalizedName = "MANAGER" },
//                new IdentityRole { Id = AUTHOR_ROLE_ID, Name = "Author", NormalizedName = "AUTHOR" }
//            );
//            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
//            {
//                RoleId = ADMIN_ROLE_ID,
//                UserId = ADMIN_ID
//            });
//            modelBuilder.Entity<ArticleCategory>().HasData(
//            new ArticleCategory { Id = 1, Title = "Content Marketing" },
//            new ArticleCategory { Id = 2, Title = "Data Analysis" },
//            new ArticleCategory { Id = 3, Title = "Digital Marketing" },
//            new ArticleCategory { Id = 4, Title = "Web Analytics" },
//            new ArticleCategory { Id = 5, Title = "Social Marketing" },
//            new ArticleCategory { Id = 6, Title = "Great Speakers" }
//            );
//            modelBuilder.Entity<Article>().HasData(
//                new Article { Id = 1, ArticleCategoryId = 1, UserId = ADMIN_ID, Title = "نحوه گرفتن اقامت رایگان", Description = "نحوه گرفتن اقامت رایگان", AddedDate = DateTime.Now }
//                );
//            modelBuilder.Entity<ArticleHeadLine>().HasData(
//                 new ArticleHeadLine { Id = 1, ArticleId = 1, Title = "شرایط ویزای استارت آپ کانادا", Description = " اثبات کنید که طرح تجاری یا ایده شما توسط یکی از سازمان های معرفی شده حمایت می شود. در این حالت این شرکت‌ها نامه‌ای مبنی بر پذیرفته شدن طرح تجاری شما خواهند داد." },
//                 new ArticleHeadLine { Id = 2, ArticleId = 1, Title = "هزینه ویزای استارت آپ کانادا", Description = "Test" }
//                 );
//            modelBuilder.Entity<ArticleComment>().HasData(
//                 new ArticleComment { Id = 1, ArticleId = 1, Name = "User", Email = "User@Comment.com", Message = "This is a test comment", AddedDate = DateTime.Now },
//                 new ArticleComment { Id = 2, ArticleId = 1, Name = "User2", Email = "User2@Comment.com", Message = "This is a test comment reply", AddedDate = DateTime.Now, ParentId = 1 }
//                );
//            modelBuilder.Entity<ArticleTag>().HasData(
//                 new ArticleTag { Id = 1, ArticleId = 1, Title = "Test Tag" }
//                );
//            modelBuilder.Entity<StaticContentType>().HasData(
//                 new StaticContentType { Id = 1, Identifier = "about-us", Name = "About Us" }
//                );
//            modelBuilder.Entity<StaticContentDetail>().HasData(
//            new StaticContentDetail { Id = 1, StaticContentTypeId = 1, Identifier = "main", Title = "SEO Agency of The Year", FieldDescription = "Finalist", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",Image="about-us.jpg" },
//            new StaticContentDetail { Id = 2, StaticContentTypeId = 1, Identifier = "first-row", Title = "Increased Traffic", FieldDescription = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt utsa." },
//            new StaticContentDetail { Id = 3, StaticContentTypeId = 1, Identifier = "second-row", Title = "Cost-Effectiveness", FieldDescription = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt utsa." },
//            new StaticContentDetail { Id = 4, StaticContentTypeId = 1, Identifier = "third-row", Title = "Increased Site Usability", FieldDescription = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt utsa." }
//                );
//            modelBuilder.Entity<Faq>().HasData(
//                new Faq { Id= 1 , Question = "What is your company philosophy?", Answer = "An Interior Designer is a trained professional who creates and designs interior spaces which are aesthetically attractive and functional.An Interior Decorator, on the other hand, views interior design with a largely cosmetic approach using decorative elements to merely rearrange existing spaces." },
//                new Faq { Id= 2 , Question = "What did you do to make it a success?", Answer = "An Interior Designer is a trained professional who creates and designs interior spaces which are aesthetically attractive and functional.An Interior Decorator, on the other hand, views interior design with a largely cosmetic approach using decorative elements to merely rearrange existing spaces." }
//                );
//            modelBuilder.Entity<OurTeam>().HasData(
//                 new OurTeam { Id = 1, Name = "NOUR ELDIN",Role = "CEO & FOUNDER", Image = "our-team-img-1.jpg" },
//                 new OurTeam { Id = 2, Name = "LES WILLIAMS", Role = "CEO & FOUNDER", Image = "our-team-img-2.jpg" },
//                 new OurTeam { Id = 3, Name = "SARA STEWART", Role = "CEO & FOUNDER", Image = "our-team-img-3.jpg" },
//                 new OurTeam { Id = 4, Name = "EMY JACMAN", Role = "CEO & FOUNDER", Image = "our-team-img-4.jpg" }
//                );
//            modelBuilder.Entity<Testimonial>().HasData(
//                 new Testimonial { Id = 1, Speaker = "Ariana Hedge CEO, Devrise", Message = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", Rate = 5 }
//                );
//            modelBuilder.Entity<Partner>().HasData(
//                 new Partner { Id = 1, Image = "partner-1.png", Title ="ThemeForest" },
//                 new Partner { Id = 2, Image = "partner-2.png", Title ="Audio Jungle" },
//                 new Partner { Id = 3, Image = "partner-3.png", Title ="Codcanyon" },
//                 new Partner { Id = 4, Image = "partner-4.png", Title ="Graphic River" }
//                );
//            modelBuilder.Entity<Gallery>().HasData(
//                 new Gallery { Id = 1, Image = "gallery-img1.jpg", Title = "Gallery Image 1" },
//                 new Gallery { Id = 2, Image = "gallery-img2.jpg", Title = "Gallery Image 2" }
//                );
//            modelBuilder.Entity<Service>().HasData(
//                 new Service { Id = 1,Thumbnail = "service-icon1.png", Image = "services-details.jpg", Title = "SEO",
//                 ShortDescription = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et",
//                 Description= "We denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleas ure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrink ing from toil and pain.",
//                 Address= "503 Old Buffalo Street Northwest#205, New York-3087",Phone= "+0123-505-6789",
//                 Email= "info@sbtechnosoft.com",
//                 FileInfo= "Impress clients new and existing with elite construction brochures. Impress clients new and existing with elite construction.",
//                 File="dummy.pdf"
//                 },
//                 new Service { Id = 2, Image = "service-icon2.png", Title = "Content Marketing",
//                 ShortDescription= "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et"},
//                 new Service { Id = 3, Image = "service-icon3.png", Title = "Data Analysis",
//                 ShortDescription= "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et"},
//                 new Service { Id = 4, Image = "service-icon4.png", Title = "Digital Marketing",
//                 ShortDescription= "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et"},
//                 new Service { Id = 5, Image = "service-icon5.png", Title = "Web Analytics",
//                 ShortDescription= "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et"},
//                 new Service { Id = 6, Image = "service-icon6.png", Title = "Social Marketing",
//                 ShortDescription= "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et"}
//                );
//            modelBuilder.Entity<ServiceInclude>().HasData(
//                 new ServiceInclude { Id = 1,ServcieId=1, Title = "Social Marketing",
//                     Description= "We denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleas ure of the moment, so blinded by desire, that they cannot foresee the pain and the trouble that are bound to ensue; and equal blame belongs to those who fail demoralized by the charms in their duty through the weakness of will so blinded by desire"
//                 }
//                );
//        }
//        public static string GetHashedPassword(User user, string password)
//        {
//            var pass = new PasswordHasher<User>();
//            var hashed = pass.HashPassword(user, password);
//            return hashed;
//        }
//    }
//}
