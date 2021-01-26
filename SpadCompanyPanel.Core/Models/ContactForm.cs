using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpadCompanyPanel.Core.Models
{
    public class ContactForm : IBaseEntity
    {
        public int Id { get; set; }
        [MaxLength(600)]
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string Name { get; set; }
        //[MaxLength(600)]
        //[Display(Name = "موبایل")]
        //[Required(ErrorMessage = "لطفا شماره {0} خود را وارد کنید")]
        //public string Phone { get; set; }
        [MaxLength(600)]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نیست")]
        public string Email { get; set; }
        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public bool IsViewed { get; set; }
        //public int ServiceId { get; set; }
        //public Service Service { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
