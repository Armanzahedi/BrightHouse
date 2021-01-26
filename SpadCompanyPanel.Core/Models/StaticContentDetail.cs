using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace SpadCompanyPanel.Core.Models
{
   public class StaticContentDetail : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
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
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        [Display(Name = "لینک")]
        public string Link { get; set; }

        public int StaticContentTypeId { get; set; }
        public StaticContentType StaticContentType { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
