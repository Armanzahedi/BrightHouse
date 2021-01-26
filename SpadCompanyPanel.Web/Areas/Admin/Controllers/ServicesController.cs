using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Web.ViewModels;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly ServicesRepository _repo;
        public ServicesController(ServicesRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/Services
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }
        // GET: Admin/Services/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service, HttpPostedFileBase ServiceImage, HttpPostedFileBase ServiceFile)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (ServiceImage != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(ServiceImage.FileName);
                    ServiceImage.SaveAs(Server.MapPath("/Files/Services/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer();
                    thumb.Resize(Server.MapPath("/Files/Services/Image/" + newFileName),
                        Server.MapPath("/Files/Services/Thumb/" + newFileName));

                    service.Image = newFileName;
                }
                #endregion

                #region Upload File
                if (ServiceFile != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(ServiceFile.FileName);
                    ServiceFile.SaveAs(Server.MapPath("/Files/Services/File/" + newFileName));
                    service.File = newFileName;
                }
                #endregion
                _repo.Add(service);

                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Admin/Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = _repo.Get(id.Value);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service, HttpPostedFileBase ServiceImage, HttpPostedFileBase ServiceFile)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (ServiceImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/Services/Image/" + service.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/Services/Image/" + service.Image));

                    if (System.IO.File.Exists(Server.MapPath("/Files/Services/Thumb/" + service.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/Services/Thumb/" + service.Image));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(ServiceImage.FileName);
                    ServiceImage.SaveAs(Server.MapPath("/Files/Services/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer();
                    thumb.Resize(Server.MapPath("/Files/Services/Image/" + newFileName), Server.MapPath("/Files/Services/Thumb/" + newFileName));
                    service.Image = newFileName;
                }
                #endregion
                #region Upload File
                if (ServiceFile != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/Services/File/" + service.File)))
                        System.IO.File.Delete(Server.MapPath("/Files/Services/File/" + service.File));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(ServiceFile.FileName);
                    ServiceFile.SaveAs(Server.MapPath("/Files/Services/File/" + newFileName));
                    service.File = newFileName;
                }
                #endregion
                _repo.Update(service);
                return RedirectToAction("Index");
            }
            return View(service);
        }
        // GET: Admin/Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = _repo.Get(id.Value);
            if (service == null)
            {
                return HttpNotFound();
            }
            return PartialView(service);
        }

        // POST: Admin/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var service = _repo.Get(id);

            //#region Delete Service Image
            //if (service.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/Services/Image/" + service.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/Services/Image/" + service.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/Services/Thumb/" + service.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/Services/Thumb/" + service.Image));
            //}
            //#endregion
            //#region Delete Service File
            //if (System.IO.File.Exists(Server.MapPath("/Files/Services/File/" + service.File)))
            //    System.IO.File.Delete(Server.MapPath("/Files/Services/File/" + service.File));
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
