using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SpadCompanyPanel.Core.Models
{
    public class RealState : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500,ErrorMessage = "{0} باید کمتر از 500 کارکتر باشد")]
        public string Title { get; set; }
        [Display(Name = "عنوان (انگلیسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} باید کمتر از 500 کارکتر باشد")]
        public string EnglishTitle { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        public int Type { get; set; }
        [Display(Name = "توضیح کوتاه")]
        [MaxLength(1000, ErrorMessage = "{0} باید کمتر از 1000 کارکتر باشد")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیح کوتاه (انگلیسی)")]
        [MaxLength(1000, ErrorMessage = "{0} باید کمتر از 1000 کارکتر باشد")]
        [DataType(DataType.MultilineText)]
        public string EnglishShortDescription { get; set; }
        [AllowHtml]
        [Display(Name = "توضیح")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [AllowHtml]
        [Display(Name = "توضیح (انگلیسی)")]
        [DataType(DataType.MultilineText)]
        public string EnglishDescription { get; set; }
        [Display(Name = "منطقه")]
        [MaxLength(500, ErrorMessage = "{0} باید کمتر از 500 کارکتر باشد")]
        public string Region { get; set; }
        [Display(Name = "منطقه (انگلیسی)")]
        [MaxLength(500, ErrorMessage = "{0} باید کمتر از 500 کارکتر باشد")]
        public string EnglishRegion { get; set; }
        [Required(ErrorMessage = "لطفا شهر را انتخاب کنید")]
        public int GeoDivisionId { get; set; }
        public GeoDivision GeoDivision { get; set; }
        [Display(Name = "امتیاز")]
        [Required(ErrorMessage = "لطفا امتیاز را وارد کنید")]
        public int Rate { get; set; }
        [AllowHtml]
        [Display(Name = "لوکیشن")]
        [DataType(DataType.MultilineText)]
        public string Location { get; set; }
        public DateTime LastViewDate { get; set; }
        public ICollection<RealStateGallery> RealStateGalleries { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
