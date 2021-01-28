using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Core.Utility;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Web.ViewModels;

namespace SpadCompanyPanel.Web.Areas.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class DashboardController : Controller
    {
        private UsersRepository _usersRepo;
        private CustomersRepository _customerRepo;
        private InvoicesRepository _invoiceRepo;
        private GeoDivisionsRepository _geoDivisonsRepo;
        public DashboardController(UsersRepository usersRepo,
            CustomersRepository customerRepo,
            InvoicesRepository invoiceRepo, 
            GeoDivisionsRepository geoDivisonsRepo)
        {
            _usersRepo = usersRepo;
            _customerRepo = customerRepo;
            _invoiceRepo = invoiceRepo;
            _geoDivisonsRepo = geoDivisonsRepo;
        }
        // GET: Customer/Dashboard
        public ActionResult Index()
        {
            var customer = _customerRepo.GetCurrentCustomer();
            var invoices = _invoiceRepo.GetCustomerInvoices(customer.Id);
            var vm = new CustomerDashboardViewModel()
            {
                Customer = customer,
                Invoices = invoices
            };
            return View(vm);
        }

        public ActionResult EditMyProfile()
        {
            var customer = _customerRepo.GetCurrentCustomer();
            if (customer == null)
            {
                return HttpNotFound();
            }

            var form = new EditCustomerViewModel()
            {
                UserId = customer.User.Id,
                UserName = customer.User.UserName,
                CustomerId = customer.Id,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                Email = customer.User.Email,
                PhoneNumber = customer.User.PhoneNumber,
                Avatar = customer.User.Avatar,
                NationalCode = customer.NationalCode,
                Address = customer.Address,
                PostalCode = customer.PostalCode
            };
            return View(form);
        }
        [HttpPost]
        public ActionResult EditMyProfile(EditCustomerViewModel form, HttpPostedFileBase UserAvatar)
        {
            if (ModelState.IsValid)
            {
                #region Check for duplicate username or email

                if (form.UserName != null)
                {
                    if (_usersRepo.UserNameExists(form.UserName, form.UserId))
                    {
                        ViewBag.Message = Resources.Resource.UserNameExits;

                        return View(form);
                    }
                }
                if (_usersRepo.PhoneNumberExists(form.PhoneNumber, form.UserId))
                {
                    ViewBag.Message = Resources.Resource.PhoneNumberExists;
                    return View(form);
                }
                if (_usersRepo.EmailExists(form.Email, form.UserId))
                {
                    ViewBag.Message = Resources.Resource.PhoneNumberExists;
                    return View(form);
                }
                #endregion

                #region Upload Image
                if (UserAvatar != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/UserAvatars/" + form.Avatar)))
                        System.IO.File.Delete(Server.MapPath("/Files/UserAvatars/" + form.Avatar));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(UserAvatar.FileName);
                    UserAvatar.SaveAs(Server.MapPath("/Files/UserAvatars/" + newFileName));

                    form.Avatar = newFileName;
                }
                #endregion

                var user = _usersRepo.GetUser(form.UserId);
                user.UserName = form.UserName ?? form.PhoneNumber;
                user.FirstName = form.FirstName;
                user.LastName = form.LastName;
                user.Email = form.Email;
                user.PhoneNumber = form.PhoneNumber;
                user.Avatar = form.Avatar;

                _usersRepo.UpdateUser(user);

                var customer = _customerRepo.Get(form.CustomerId.Value);

                customer.NationalCode = form.NationalCode;
                customer.Address = form.Address;
                customer.PostalCode = form.PostalCode;
                _customerRepo.Update(customer);

                return RedirectToAction("Index");
            }
            return View(form);

        }
        public ActionResult ResetMyPassword(string id)
        {
            ViewBag.Message = null;
            ViewBag.UserId = id;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetMyPassword(ResetCustomerPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var validatePassword = await _usersRepo.ValidatePassword(model.OldPassword);
                if (validatePassword.Succeeded)
                {
                    var result = await _usersRepo.SetNewPassword(model.UserId, model.OldPassword, model.Password);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                }

                ViewBag.Message = Resources.Resource.IncorrectPassword;
                ViewBag.UserId = model.UserId;
            }
            return View(model);
        }

        public ActionResult Invoices()
        {
            var customer = _customerRepo.GetCurrentCustomer();
            var invoices = _invoiceRepo.GetCustomerInvoices(customer.Id);
            return View(invoices);
        }
    }
}