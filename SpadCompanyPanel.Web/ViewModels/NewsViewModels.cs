using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadCompanyPanel.Web.ViewModels
{
    public class NewsFormViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان مطلب")]
        [MaxLength(600, ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیح")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        public int NewsCategoryId { get; set; }
        public HttpPostedFileBase NewsImage { get; set; }

        public List<NewsHeadLineViewModel> NewsHeadLines { get; set; }
    }
    public class NewsHeadLineViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
    }

    public class NewsInfoViewModel
    {
        public NewsInfoViewModel()
        {
            
        }
        public NewsInfoViewModel(News News)
        {
            this.Id = News.Id;
            this.Title = News.Title;
            this.Author = News.User != null ? $"{News.User.FirstName} {News.User.LastName}" : "-";
            this.PersianAddedDate = News.AddedDate != null ? new PersianDateTime(News.AddedDate.Value).ToString() : "-";
        }
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "نویسنده")]
        public string Author { get; set; }
        [Display(Name = "دسته بندی")]
        public string NewsCategory { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public string PersianAddedDate { get; set; }
    }
 
    public class TopNewsViewModel
    {
        public TopNewsViewModel()
        {
        }

        public TopNewsViewModel(News News)
        {
            this.Id = News.Id;
            this.Title = News.Title;
            this.Image = News.Image;
            this.PersianDate = News.AddedDate != null ? new PersianDateTime(News.AddedDate.Value).ToString("d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string PersianDate { get; set; }
    }
    public class NewsListViewModel
    {
        public NewsListViewModel()
        {
        }

        public NewsListViewModel(News News)
        {
            this.Id = News.Id;
            this.Title = News.Title;
            this.ShortDescription = News.ShortDescription;
            this.Author = News.User != null ? $"{News.User.FirstName} {News.User.LastName}" : "-";
            this.Image = News.Image;
            this.AuthorAvatar = News.User.Avatar ?? "user-avatar.png";
            this.PersianDate = News.AddedDate != null ? new PersianDateTime(News.AddedDate.Value).ToString("d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public string PersianDate { get; set; }
        public string Author { get; set; }
        public string AuthorAvatar { get; set; }
        public string Role { get; set; }
    }

    public class NewsCategoriesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NewsCount { get; set; }
    }

    public class NewsDetailsViewModel
    {
        public NewsDetailsViewModel()
        {

        }
        public NewsDetailsViewModel(News News)
        {
            this.Id = News.Id;
            this.Title = News.Title;
            this.Image = News.Image;
            this.ShortDescription = News.ShortDescription;
            this.Description = News.Description;
            this.Author = News.User != null ? $"{News.User.FirstName} {News.User.LastName}" : "-";
            this.PersianDate = News.AddedDate != null ? new PersianDateTime(News.AddedDate.Value).ToString("dddd d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string PersianDate { get; set; }
    }

    public class LatestNewsViewModel
    {
        public LatestNewsViewModel()
        {
        }

        public LatestNewsViewModel(News News)
        {
            this.Id = News.Id;
            this.Title = News.Title;
            this.Image = News.Image;
            this.PersianDate = News.AddedDate != null ? new PersianDateTime(News.AddedDate.Value).ToString("d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string PersianDate { get; set; }
    }
}