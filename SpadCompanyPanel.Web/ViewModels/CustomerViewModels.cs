using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SpadCompanyPanel.Core.Models;

namespace SpadCompanyPanel.Web.ViewModels
{
    public class CustomerDashboardViewModel
    {
        public Customer Customer { get; set; }
        public List<CustomerInvoiceViewModel> Invoices { get; set; }
    }
    public class CustomerInvoiceViewModel
    {
        public CustomerInvoiceViewModel()
        {

        }

        public CustomerInvoiceViewModel(Invoice invoice)
        {
            this.Id = invoice.Id;
            this.InvoiceNumber = invoice.InvoiceNumber;
            this.IsPayed = invoice.IsPayed;
            this.RegisteredDate = new PersianDateTime(invoice.AddedDate).ToString("dddd d MMMM yyyy");
        }
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string RegisteredDate { get; set; }
        public string Price { get; set; }
        public bool IsPayed { get; set; }
    }
    public class RegisterCustomerViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string LastName { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل 6 کارکتر باشد", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "پسورد باید حداقل 6 کارکتر و شامل یک حرف بزرگ یک حرف کوچک یک عدد و یک کارکتر خاص باشد.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "عدم تطابق رمز عبور جدید و تکرار رمز عبور جدید")]
        public string ConfirmPassword { get; set; }
    }
}