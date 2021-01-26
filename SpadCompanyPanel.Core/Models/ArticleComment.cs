using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpadCompanyPanel.Core.Models
{
    public class ArticleComment : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} باید کمتر از 300 کارکتر باشد")]
        public string Name { get; set; }
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل نا معتبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "{0} باید کمتر از 400 کارکتر باشد")]
        public string Email { get; set; }
        [Display(Name = "پیام")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(800, ErrorMessage = "{0} باید کمتر از 800 کارکتر باشد")]
        public string Message { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? AddedDate { get; set; }
        public int? ParentId { get; set; }
        public virtual ArticleComment Parent { get; set; }
        public virtual ICollection<ArticleComment> Children { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
