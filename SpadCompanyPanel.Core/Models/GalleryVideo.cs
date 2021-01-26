using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class GalleryVideo : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "ویدئو")]
        public string Video { get; set; }
        [Display(Name = "تصویر بند انگشتی")]
        public string Image { get; set; }
        [Display(Name = "عنوان ویدئو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
