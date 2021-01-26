using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class Option : IBaseEntity
    {
        public int Id { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} باید کمتر از 500 کارکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "عنوان (انگلیسی)")]
        [MaxLength(400, ErrorMessage = "نام دسته باید از 400 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا نام دسته را وارد کنید")]
        public string EnglishTitle { get; set; }
    }
}
