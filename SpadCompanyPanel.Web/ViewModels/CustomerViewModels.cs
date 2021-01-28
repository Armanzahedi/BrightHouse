using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Web.Resources;

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
            //this.IsPayed = invoice.IsPayed;
            //this.RegisteredDate = new PersianDateTime(invoice.AddedDate).ToString("dddd d MMMM yyyy");
        }
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string RegisteredDate { get; set; }
        public string Price { get; set; }
        public bool IsPayed { get; set; }
    }
    public class LoginCustomerViewModel
    {
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PhoneNumberRequired")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordRequired")]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resource))]
        public bool RememberMe { get; set; }
    }
    public class RegisterCustomerViewModel
    {
        [Display(Name = "FirstName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FirstNameRequired")]
        public string FirstName { get; set; }
        [Display(Name = "LastName", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "LastNameRequired")]
        public string LastName { get; set; }
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PhoneNumberRequired")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailInvalid")]
        public string Email { get; set; }
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordIsLessThanSix", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordIsNotStrong")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ReTypePassword", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ReTypePasswordRequired")]
        [Compare("Password", ErrorMessage = "RetypedPasswordIsNotCorrect")]
        public string ConfirmPassword { get; set; }
    }
}