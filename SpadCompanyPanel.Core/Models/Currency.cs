using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class Currency : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} باید کمتر از 300 کارکتر باشد")]
        public string Name { get; set; }
        [Display(Name = "واحد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(10, ErrorMessage = "{0} باید کمتر از 10 کارکتر باشد")]
        public string Unit { get; set; }
        [Display(Name = "نرخ تبدیل (نسبت به یورو)")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float ExchangeRate { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
