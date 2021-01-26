using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class Customer : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "کد ملی")]
        [MaxLength(100, ErrorMessage = "{0} باید کمتر از 100 کارکتر باشد")]
        public string NationalCode { get; set; }
        [Display(Name = "آدرس")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Display(Name = "کد پستی")]
        [MaxLength(100, ErrorMessage = "{0} باید کمتر از 100 کارکتر باشد")]
        public string PostalCode { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int? GeoDivisionId { get; set; }
        public GeoDivision GeoDivision { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
