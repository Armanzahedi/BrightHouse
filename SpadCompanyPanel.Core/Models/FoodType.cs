using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class FoodType : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "نوع غذا")]
        [MaxLength(600, ErrorMessage = "نوع غذا باید از 600 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "لطفا نوع غذا را وارد کنید")]
        public string Title { get; set; }
        public ICollection<Food> Foods { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
