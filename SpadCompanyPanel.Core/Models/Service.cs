using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace SpadCompanyPanel.Core.Models
{
    public class Service : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "عنوان سرویس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(600, ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        public string Title { get; set; }
        [Display(Name = "توضیح کوتاه")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیح")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "آدرس")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Display(Name = "تلفن")]
        [DataType(DataType.MultilineText)]
        public string Phone { get; set; }
        [Display(Name = "ایمیل")]
        [DataType(DataType.MultilineText)]
        public string Email { get; set; }
        [Display(Name = "اطلاعات فایل")]
        [DataType(DataType.MultilineText)]
        public string FileInfo { get; set; }
        [Display(Name = "فایل")]
        public string File { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        public ICollection<ServiceInclude> ServiceIncludes { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
