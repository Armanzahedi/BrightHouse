using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpadCompanyPanel.Core.Models
{
    public class ArticleCategory : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name ="نام دسته")]
        [MaxLength(400,ErrorMessage = "نام دسته باید از 400 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا نام دسته را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "نام دسته (انگلیسی)")]
        [MaxLength(400, ErrorMessage = "نام دسته باید از 400 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا نام دسته را وارد کنید")]
        public string EnglishTitle { get; set; }
        public ICollection<Article> Articles { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
