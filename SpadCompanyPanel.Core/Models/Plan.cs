using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SpadCompanyPanel.Core.Models
{
    public class Plan : IBaseEntity
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} باید کمتر از 500 کارکتر باشد")]
        public string Title { get; set; }
        [Display(Name = "عنوان (انگلیسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} باید کمتر از 500 کارکتر باشد")]
        public string EnglishTitle { get; set; }
        [Display(Name = "تعداد اتاق")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Rooms { get; set; }
        [Display(Name = "مساحت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Area { get; set; }
        [Display(Name = "تعداد سرویس بهداشتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BathRooms { get; set; }
        [Display(Name = "طبقه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FloorNo { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        [Display(Name = "قیمت (یورو)")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float Price { get; set; }
        [Display(Name = "قیمت اجاره")]
        public float? RentPrice { get; set; }
        public int PlanType { get; set; }
        public int RealStateId { get; set; }
        public RealState RealState { get; set; }
        [Display(Name = "توضیحات پلان")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "توضیحات پلان (انگلیسی)")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string EnglishDescription { get; set; }
        [Display(Name = "تصویر پلان")]
        public string Image { get; set; }
        [Display(Name = "ویدئو پلان")]
        public string Video { get; set; }
        [Display(Name = "تصویر بند انگشتی ویدئو")]
        public string VideoThumbnail { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
