using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class Invoice : IBaseEntity
    {
        public int Id { get; set; }
        [DisplayName("قیمت")]
        public long TotalPrice { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int? PaymentAccountId { get; set; }
        public PaymentAccount PaymentAccount { get; set; }
        [DisplayName("تاریخ ثبت")]
        public DateTime RegisteredDate { get; set; }
        [DisplayName("تاریخ پرداخت")]
        public DateTime? PaymentDate { get; set; }
        [DisplayName("تاریخ تائید پرداخت")]
        public DateTime? PaymentConfirmDate { get; set; }
        [DisplayName("شماره سفارش")]
        public string InvoiceNumber { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? PlanId { get; set; }
        public Plan Plan { get; set; }
        public string Description { get; set; }
        [DisplayName("وضغیت پرداخت")]
        public int PaymentStatus { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
