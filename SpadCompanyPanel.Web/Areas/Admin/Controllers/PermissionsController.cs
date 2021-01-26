using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PermissionsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Admin/Permissions
        public ActionResult Index()
        {
            return View(db.Permissions.ToList().OrderBy(a => a.Title));
        }


        public ActionResult DeclaretionControllersActions()
        {
            #region Found Controllers and Actions

            var allTypes =
               from asm in AppDomain.CurrentDomain.GetAssemblies()
               let types = (from type in asm.GetTypes()
                            let contname = type.Name.Replace("Controller", "")
                            let methods = (from method in type.GetMethods()
                                           let hasHttpPost = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Length > 0
                                           where (method.ReturnType.IsSubclassOf(typeof(ActionResult)) || method.ReturnType == typeof(ActionResult)) && !hasHttpPost
                                           select new
                                           {
                                               Controller = contname,
                                               Action = method.Name
                                           }
                            )
                            where type.IsSubclassOf(typeof(Controller))
                            select new { Name = contname, Controllers = methods })
               select new { asm.FullName, AllControllers = types };


            #endregion


            #region Add in Permission Table

            foreach (var controllers in allTypes)
            {
                //int i = 0;
                foreach (var controller in controllers.AllControllers)
                {

                    //@i - @controller.Name
                    #region Add a record : Only Controller
                    Permission pController = new Permission()
                    {
                        ControllerName = controller.Name,
                        Title = controller.Name,
                        Name = controller.Name.Trim().ToLower(),
                        ParentId = null,
                        DisplayPriority = 1
                    };
                    db.Permissions.Add(pController);
                    db.SaveChanges();
                    #endregion

                    foreach (var controlleraction in controller.Controllers)
                    {

                        //  @controlleraction.Action
                        #region Add a record : Controller + Actions
                        Permission pAction = new Permission()
                        {
                            ControllerName = controller.Name,
                            ActionName = controlleraction.Action,
                            Title = controller.Name + " " + controlleraction.Action,
                            Name = controller.Name.Trim().ToLower() + controlleraction.Action.Trim().ToLower(),
                            ParentId = pController.Id,
                            DisplayPriority = 2
                        };
                        db.Permissions.Add(pAction);
                        db.SaveChanges();
                        #endregion

                    }
                }

                // i++;
            }


            #endregion

            return View();

        }


        public ActionResult ThisControlerActions()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThisControlerActions(string controllerName)
        {


            #region Found Controllers and Actions

            var allTypes =
               from asm in AppDomain.CurrentDomain.GetAssemblies()
               let types = (from type in asm.GetTypes()
                            let contname = type.Name.Replace("Controller", "")
                            let methods = (from method in type.GetMethods()
                                           let hasHttpPost = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Length > 0
                                           where (method.ReturnType.IsSubclassOf(typeof(ActionResult)) || method.ReturnType == typeof(ActionResult)) && !hasHttpPost
                                           select new
                                           {
                                               Controller = contname,
                                               Action = method.Name
                                           }
                            )
                            where type.IsSubclassOf(typeof(Controller))
                            select new { Name = contname, Controllers = methods })
               select new { asm.FullName, AllControllers = types };


            #endregion


            #region Add in Permission Table

            foreach (var controllers in allTypes)
            {
                //int i = 0;
                foreach (var controller in controllers.AllControllers.Where(a => a.Name == controllerName))
                {

                    //@i - @controller.Name
                    #region Add a record : Only Controller
                    Permission pController = new Permission()
                    {
                        ControllerName = controller.Name,
                        Title = controller.Name,
                        Name = controller.Name.Trim().ToLower(),
                        ParentId = null,
                        DisplayPriority = 1
                    };
                    db.Permissions.Add(pController);
                    db.SaveChanges();
                    #endregion

                    foreach (var controlleraction in controller.Controllers)
                    {

                        //  @controlleraction.Action
                        #region Add a record : Controller + Actions
                        Permission pAction = new Permission()
                        {
                            ControllerName = controller.Name,
                            ActionName = controlleraction.Action,
                            Title = controller.Name + " " + controlleraction.Action,
                            Name = controller.Name.Trim().ToLower() + controlleraction.Action.Trim().ToLower(),
                            ParentId = pController.Id,
                            DisplayPriority = 2
                        };
                        db.Permissions.Add(pAction);
                        db.SaveChanges();
                        #endregion

                    }
                }

                // i++;
            }


            #endregion




            ViewBag.Message = controllerName + " Actions Added";
            return View();
        }
        public ActionResult AddAminUserAllPermissions()
        {
            //List<RolePersmissions> rp=new List<RolePersmissions>();
            foreach (var item in db.Permissions.ToList())
            {
                var role = new RolePermission
                {
                    Id = 1,
                    PermissionId = item.Id
                };
                db.RolePermissions.Add(role);
            }
            db.SaveChanges();

            return View();
        }



        // GET: Admin/Permissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permissions = db.Permissions.Find(id);
            if (permissions == null)
            {
                return HttpNotFound();
            }
            return View(permissions);
        }

        // GET: Admin/Permissions/Create
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.Permissions.Where(a => a.ParentId == null).ToList().OrderBy(a => a.ControllerName), "Id", "Title");
            return View();
        }

        // POST: Admin/Permissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Permission permissions)
        {
            if (ModelState.IsValid)
            {
                db.Permissions.Add(permissions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentID = new SelectList(db.Permissions.Where(a => a.ParentId == null), "Id", "Title", permissions.ParentId);
            return View(permissions);
        }

        // GET: Admin/Permissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permissions = db.Permissions.Find(id);
            if (permissions == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = new SelectList(db.Permissions.Where(a => a.ParentId == null), "Id", "Title", permissions.ParentId);
            return View(permissions);
        }

        // POST: Admin/Permissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Permission permissions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permissions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(db.Permissions.Where(a => a.ParentId == null), "Id", "Title", permissions.ParentId);
            return View(permissions);
        }

        // GET: Admin/Permissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permission permissions = db.Permissions.Find(id);
            if (permissions == null)
            {
                return HttpNotFound();
            }
            return PartialView(permissions);
        }

        // POST: Admin/Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permission permissions = db.Permissions.Find(id);
            db.Permissions.Remove(permissions);
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
