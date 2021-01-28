using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpadCompanyPanel.Web.ViewModels
{
    public class InvoiceTableViewModel
    {
        public InvoiceTableViewModel()
        {
        }
        public InvoiceTableViewModel(Invoice invoice)
        {
            this.Invoice = invoice;
            this.PersianDate = new PersianDateTime(invoice.RegisteredDate).ToString();
            this.CustomerName = $"{invoice.Customer.User.FirstName} {invoice.Customer.User.LastName}";
        }
        public Invoice Invoice { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public string PersianDate { get; set; }

        public string CustomerName { get; set; }
    }
}