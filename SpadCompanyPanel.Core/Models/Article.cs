using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace SpadCompanyPanel.Core.Models
{
    public class Article : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "عنوان مطلب")]
        [MaxLength(600,ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "عنوان مطلب (انگلیسی)")]
        [MaxLength(600, ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EnglishTitle { get; set; }

        [Display(Name = "توضیح کوتاه")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیح کوتاه (انگلیسی)")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string EnglishShortDescription { get; set; }
        [Display(Name = "توضیح")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "توضیح (انگلیسی)")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string EnglishDescription { get; set; }
        public int ViewCount { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        public DateTime? AddedDate { get; set; }

        public int? ArticleCategoryId { get; set; }
        public ArticleCategory ArticleCategory { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<ArticleHeadLine> ArticleHeadLines { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
        public ICollection<ArticleComment> ArticleComments { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
