using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Core.Utility;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Web.ViewModels;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomersRepository _repo;
        private readonly UsersRepository _usersRepo;
        private readonly GeoDivisionsRepository _geoDivisonsRepo;

        public CustomersController(CustomersRepository repo, UsersRepository usersRepo, GeoDivisionsRepository geoDivisonsRepo)
        {
            _repo = repo;
            _usersRepo = usersRepo;
            _geoDivisonsRepo = geoDivisonsRepo;
        }

        // GET: Admin/Customers
        public ActionResult Index()
        {
            return View(_repo.GetCustomerTable());
        }

        public ActionResult Create()
        {
            ViewBag.Message = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddCustomerViewModel form, HttpPostedFileBase UserAvatar)
        {
            if (ModelState.IsValid)
            {
                #region Check for duplicate username or email

                if (form.UserName != null)
                {
                    if (_usersRepo.UserNameExists(form.UserName))
                    {
                        ViewBag.Message = "کاربر دیگری با همین نام کاربری در سیستم ثبت شده";

                        return View(form);
                    }
                }
                if (_usersRepo.PhoneNumberExists(form.PhoneNumber))
                {
                    ViewBag.Message = "کاربر دیگری با همین شماره تلفن در سیستم ثبت شده";

                    return View(form);
                }
                if (_usersRepo.EmailExists(form.Email))
                {
                    ViewBag.Message = "کاربر دیگری با همین ایمیل در سیستم ثبت شده";

                    return View(form);
                }
                #endregion

                #region Upload Image
                if (UserAvatar != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(UserAvatar.FileName);
                    UserAvatar.SaveAs(Server.MapPath("/Files/UserAvatars/" + newFileName));

                    form.Avatar = newFileName;
                }
                #endregion

                var userModel = new User()
                {
                    UserName = form.UserName,
                    FirstName = form.FirstName,
                    LastName = form.LastName,
                    Email = form.Email,
                    PhoneNumber = form.PhoneNumber,
                    Avatar = form.Avatar
                };
                userModel.UserName = form.UserName ?? form.PhoneNumber;
                _usersRepo.CreateUser(userModel, form.Password);
                _usersRepo.AddUserRole(userModel.Id, StaticVariables.CustomerRoleId);

                var customer = new Core.Models.Customer()
                {
                    UserId = userModel.Id,
                    IsDeleted = false,
                    NationalCode = form.NationalCode,
                    Address = form.Address,
                    PostalCode = form.PostalCode,
                };
                _repo.Add(customer);

                return RedirectToAction("Index");
            }

            return View(form);
        }

        public ActionResult Edit(int id)
        {
            var customer = _repo.GetCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var form = new EditCustomerViewModel(){
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
                PostalCode = customer.PostalCode,
            };
            return View(form);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCustomerViewModel form, HttpPostedFileBase UserAvatar)
        {

            if (ModelState.IsValid)
            {
                #region Check for duplicate username or email

                if (form.UserName != null)
                {
                    if (_usersRepo.UserNameExists(form.UserName, form.UserId))
                    {
                        ViewBag.Message = "کاربر دیگری با همین نام کاربری در سیستم ثبت شده";

                        return View(form);
                    }
                }
                if (_usersRepo.PhoneNumberExists(form.PhoneNumber, form.UserId))
                {
                    ViewBag.Message = "کاربر دیگری با همین شماره تلفن در سیستم ثبت شده";
                    return View(form);
                }
                if (_usersRepo.EmailExists(form.Email, form.UserId))
                {
                    ViewBag.Message = "کاربر دیگری با همین ایمیل در سیستم ثبت شده";
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

                var customer = _repo.Get(form.CustomerId.Value);

                customer.NationalCode = form.NationalCode;
                customer.Address = form.Address;
                customer.PostalCode = form.PostalCode;
                _repo.Update(customer);

                return RedirectToAction("Index");
            }
            return View(form);

        }

        public ActionResult Delete(int id)
        {
            var user = _repo.GetCustomer(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}