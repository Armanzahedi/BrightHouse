using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpadCompanyPanel.Core;

namespace SpadCompanyPanel.Core.Models
{
    public class GeoDivision : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [MaxLength(200)]
        public string Title { get; set; }
        [Display(Name = "عنوان (انگلیسی)")]
        [MaxLength(200)]
        public string LatinTitle { get; set; }

        public int GeoDivisionType { get; set; }
        public int? IsLeaf { get; set; }
        public int? IsArchived { get; set; }
        public int? ParentId { get; set; }
        public virtual GeoDivision Parent { get; set; }
        public virtual ICollection<GeoDivision> Children { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
