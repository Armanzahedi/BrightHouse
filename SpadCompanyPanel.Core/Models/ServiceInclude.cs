using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace SpadCompanyPanel.Core.Models
{
    public class ServiceInclude : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "عنوان سرویس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(700, ErrorMessage = "{0} باید از 700 کارکتر کمتر باشد")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیح")]
        [AllowHtml]
        public string Description { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
