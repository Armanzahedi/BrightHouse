using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Web.ViewModels;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UsersRepository _repo;
        public UsersController(UsersRepository repo)
        {
            _repo = repo;
        }

        // GET: Users
        public ActionResult Index()
        {

            return View(_repo.GetUsers());
        }

        public ActionResult Create()
        {
            ViewBag.Message = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddUserViewModel form,HttpPostedFileBase UserAvatar)
        {
            if (ModelState.IsValid)
            {
                #region Check for duplicate username or email
                if (_repo.UserNameExists(form.User.UserName))
                {
                    ViewBag.Message = "کاربر دیگری با همین نام در سیستم ثبت شده";
                    return View(form);
                }
                if (_repo.EmailExists(form.User.Email))
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

                    form.User.Avatar = newFileName;
                }
                #endregion
                var userModel = form.User;

                _repo.CreateUser(userModel, form.Password);
                return RedirectToAction("Index");
            }

            return View(form);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User users = _repo.GetUser(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User users, HttpPostedFileBase UserAvatar)
        {

            if (ModelState.IsValid)
            {
                #region Check for duplicate username or email
                if (_repo.UserNameExists(users.UserName,users.Id))
                {
                    ViewBag.Message = "کاربر دیگری با همین نام در سیستم ثبت شده";
                    return View(users);
                }
                if (_repo.EmailExists(users.Email,users.Id))
                {   
                    ViewBag.Message = "کاربر دیگری با همین ایمیل در سیستم ثبت شده";
                    return View(users);
                }
                #endregion

                #region Upload Image
                if (UserAvatar != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/UserAvatars/" + users.Avatar)))
                        System.IO.File.Delete(Server.MapPath("/Files/UserAvatars/" + users.Avatar));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(UserAvatar.FileName);
                    UserAvatar.SaveAs(Server.MapPath("/Files/UserAvatars/" + newFileName));

                    users.Avatar = newFileName;
                }
                #endregion
                _repo.UpdateUser(users);
                return RedirectToAction("Index");
            }

            return View(users);

        }
        public ActionResult EditMyProfile()
        {
            var currentUserId = CheckPermission.GetCurrentUserId();
            if (currentUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _repo.GetUser(currentUserId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMyProfile(User users, HttpPostedFileBase UserAvatar)
        {

            if (ModelState.IsValid)
            {
                #region Check for duplicate username or email
                if (_repo.UserNameExists(users.UserName, users.Id))
                {
                    ViewBag.Message = "کاربر دیگری با همین نام در سیستم ثبت شده";
                    return View(users);
                }
                if (_repo.EmailExists(users.Email, users.Id))
                {
                    ViewBag.Message = "کاربر دیگری با همین ایمیل در سیستم ثبت شده";
                    return View(users);
                }
                #endregion

                #region Upload Image
                if (UserAvatar != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/UserAvatars/" + users.Avatar)))
                        System.IO.File.Delete(Server.MapPath("/Files/UserAvatars/" + users.Avatar));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(UserAvatar.FileName);
                    UserAvatar.SaveAs(Server.MapPath("/Files/UserAvatars/" + newFileName));

                    users.Avatar = newFileName;
                }
                #endregion
                _repo.UpdateUser(users);
                return RedirectToAction("Index","Dashboard");
            }

            return View(users);

        }
        public ActionResult ResetMyPassword(string id)
        {
            ViewBag.Message = null;
            ViewBag.UserId = id;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetMyPassword(ResetMyPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var validatePassword = await _repo.ValidatePassword(model.OldPassword);
                if (validatePassword.Succeeded)
                {
                    var result = await _repo.SetNewPassword(model.UserId, model.OldPassword, model.Password);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                }

                ViewBag.Message = "رمز عبور وارد شده صحیح نیست";
                ViewBag.UserId = model.UserId;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IdentityResult> ResetPasswordToDefault(string id)
        {
            var result = await _repo.ResetPasswordToDefault(id);
            return result;
        }
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _repo.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = _repo.GetUser(id);

            #region Delete User Avatar
            if (user.Avatar != null)
            {
                if (System.IO.File.Exists(Server.MapPath("/Files/UserAvatars/" + user.Avatar)))
                    System.IO.File.Delete(Server.MapPath("/Files/UserAvatars/" + user.Avatar));

            }
            #endregion

            _repo.DeleteUser(id);
            return RedirectToAction("Index");
        }

        //public ActionResult EditMyProfile()
        //{
        //    var email = Session["UserEmail"];
        //    var user = db.UsersRepository.Get().FirstOrDefault(u => u.Email.Trim().ToLower() == email.ToString().Trim().ToLower());
        //    return View(user);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditMyProfile(Users users, string confirmPassword)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        if (users.Password == confirmPassword)
        //        {
        //            users.Password = PasswordHelper.base64Encode(users.Password);
        //            db.UsersRepository.Update(users);
        //            db.UsersRepository.Save();
        //            return RedirectToAction("Index","Home");

        //        }
        //        else
        //        {
        //            ViewBag.Message = "عدم تطابق رمز عبور و تکرار رمز";
        //        }

        //    }

        //    return View(users);

        //}
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Users users = db.UsersRepository.getById(id);
        //    if (users == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(users);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Users users = db.UsersRepository.getById(id);
        //    db.UsersRepository.Delete(users);
        //    db.UsersRepository.Save();
        //    return RedirectToAction("Index");
        //}
        public ActionResult UserRoles(string userId)
        {
            #region Create User Roles
            List<UserRolesViewModel> userRoles = new List<UserRolesViewModel>();
            foreach (var item in _repo.GetRoles())
            {
                UserRolesViewModel role = new UserRolesViewModel()
                {
                    RoleId = item.Id,
                    RoleName = item.Name,
                    Access = _repo.UserHasRole(userId,item.Id)
                };
                userRoles.Add(role);
            }
            #endregion

            var user = _repo.GetUser(userId);
            ViewBag.UserID = userId;
            ViewBag.UserName = user.UserName;

            ViewBag.TreeItems = userRoles.Select(r => new TreeViewItemModel()
            {
                Text = r.RoleName,
                Id = r.RoleId.ToString(),
                Checked = r.Access
            }).ToList();

            return View(userRoles);
        }

        [HttpPost]
        public ActionResult UserRoles(string userId, string[] selectedRoles)
        {

            if (selectedRoles == null)
            {
                return RedirectToAction("UserRoles", new { userId });
            }
            List<string> seletRoleIds = new List<string>();
            seletRoleIds.AddRange(selectedRoles);
            foreach (var role in _repo.GetRoles())
            {
                #region Add Role if is in selectRoles and is not in UserRoles

                if (seletRoleIds.Contains(role.Id) && !_repo.UserHasRole(userId, role.Id))
                {
                    _repo.AddUserRole(userId, role.Id);
                }
                #endregion

                #region Delete Role if is in UserRoles  and is not in selectRoles
                if (!seletRoleIds.Contains(role.Id) && _repo.UserHasRole(userId, role.Id))
                {
                    UserRole uRole = _repo.GetUserRole(userId,role.Id);
                    _repo.DeleteUserRole(uRole);
                }
                #endregion
            }

            return RedirectToAction("Index");
        }
    }
}
