using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class RealStateGallery : IBaseEntity
    {
        public int Id { get; set; }
        public int? RealStateId { get; set; }
        public RealState RealState { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        [Display(Name = "عنوان تصویر")]
        [MaxLength(500, ErrorMessage = "{0} باید کمتر از 500 کارکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
