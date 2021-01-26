using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Infrastructure;
using Kendo.Mvc.UI;
using SpadCompanyPanel.Web.ViewModels;
using SpadCompanyPanel.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Admin/Roles
        public ActionResult Index()
        {
            return View(db.Role.ToList());
        }

        public ActionResult RolePermission(string roleId)
        {
            #region Create Role Permissions

            List<RolePermissionsViewModel> rolePermissions = new List<RolePermissionsViewModel>();
            foreach (var item in db.Permissions.ToList())
            {
                if (item.ParentId != null)
                {
                    RolePermissionsViewModel permission = new RolePermissionsViewModel()
                    {
                        PermissionID = item.Id,
                        PermissionTitle = item.Title,
                        ControllerName = item.ControllerName,
                        Access = db.RolePermissions
                            .Where(a => a.RoleId == roleId && a.PermissionId == item.Id).Any()
                    };
                    rolePermissions.Add(permission);
                }
            }

            #endregion

            ViewBag.RoleName = db.Roles.Find(roleId).Name;
            ViewBag.RoleID = roleId;

            ViewBag.TreeItems = rolePermissions.OrderBy(a => a.PermissionTitle).Select(r => new TreeViewItemModel()
            {
                Text = r.PermissionTitle,
                Id = r.PermissionID.ToString(),
                Checked = r.Access
            }).ToList();

            return View(rolePermissions);
        }



        [HttpPost]
        public ActionResult RolePermission(string roleId, string[] selectedPermissions)
        {

            if (selectedPermissions == null)
            {
                return RedirectToAction("RolePermission", new {roleId});
            }

            List<int> selectPermissions = new List<int>();
            selectPermissions.AddRange(selectedPermissions.Select(ro => Convert.ToInt32(ro)));

            foreach (var permit in db.Permissions.ToList())
            {
                #region Add permission if is in selectedPermissions and is not in RolePermissions

                if (selectPermissions.Contains(permit.Id) && !db.RolePermissions
                        .Where(a => a.RoleId == roleId && a.PermissionId == permit.Id).Any())
                {
                    RolePermission rPermit = new RolePermission()
                    {
                        RoleId = roleId,
                        PermissionId = permit.Id
                    };
                    db.RolePermissions.Add(rPermit);
                    db.SaveChanges();
                }

                #endregion

                #region Delete Role if is in UserRoles  and is not in selectRoles

                if (!selectPermissions.Contains(permit.Id) && db.RolePermissions
                        .Where(a => a.RoleId == roleId && a.PermissionId == permit.Id).Any())
                {
                    RolePermission rPermision = db.RolePermissions
                        .Where(a => a.RoleId == roleId && a.PermissionId == permit.Id).FirstOrDefault();
                    db.RolePermissions.Remove(rPermision);
                    db.SaveChanges();
                }

                #endregion
            }

            return RedirectToAction("Index");
        }


        // GET: Admin/Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Role roles = db.Role.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }

            return View(roles);
        }

        // GET: Admin/Roles/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role roles)
        {
            if (ModelState.IsValid)
            {
                db.Role.Add(roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roles);
        }

        // GET: Admin/Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Role roles = db.Role.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }

            return PartialView(roles);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roles);
        }

        // GET: Admin/Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Role roles = db.Role.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }

            return PartialView(roles);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Role roles = db.Role.Find(id);
            db.Roles.Remove(roles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}

