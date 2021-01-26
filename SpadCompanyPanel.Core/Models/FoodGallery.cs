using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
   public class FoodGallery : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        [Display(Name = "عنوان غذا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //public int? FoodId { get; set; }
        //public Food Food { get; set; }
        public string Title { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
