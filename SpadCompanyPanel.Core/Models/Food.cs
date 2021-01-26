using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SpadCompanyPanel.Core.Models
{
    public class Food : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "عنوان غذا")]
        [MaxLength(600, ErrorMessage = "{0} باید از 600 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیح کوتاه")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیح")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        public int? FoodTypeId { get; set; }
        public FoodType FoodType { get; set; }

        public ICollection<FoodGallery> FoodGalleries { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
