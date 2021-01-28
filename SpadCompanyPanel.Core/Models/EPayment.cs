using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class EPayment : IBaseEntity
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int PaymentAccountId { get; set; }
        public PaymentAccount PaymentAccount { get; set; }
        public float Amount { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        [MaxLength(255)]
        public string ExtraInfo { get; set; }
        public string Token { get; set; }
        public string Url { get; set; }
        public string PaymentKey { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
