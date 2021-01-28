using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class PaymentAccount : IBaseEntity
    {
        public int Id {get; set;}
        public string Title { get; set; }
        public string QrCode { get; set; }
        public string Address { get; set; }
        public string InsertUser {get; set;}
        public DateTime? InsertDate {get; set;}
        public string UpdateUser {get; set;}
        public DateTime? UpdateDate {get; set;}
        public bool IsDeleted {get; set;}
    }
}
