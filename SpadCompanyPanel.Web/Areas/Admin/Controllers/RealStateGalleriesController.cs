using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    public class RealStateGalleriesController : Controller
    {
        private readonly RealStateGalleriesRepository _repo;
        public RealStateGalleriesController(RealStateGalleriesRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index(int realStateId)
        {
            ViewBag.RealStateName = _repo.GetRealStateName(realStateId);
            ViewBag.RealStateId = realStateId;
            var galleries = _repo.GetRealStateGalleries(realStateId);
            return View(galleries);
        }

        public ActionResult Create(int realStateId)
        {
            ViewBag.RealStateId = realStateId;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RealStateGallery realStateGallery, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (Image != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(1170, 620, true);
                    image.Resize(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName),
                        Server.MapPath("/Files/RealStateImages/Gallery/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));

                    realStateGallery.Image = newFileName;
                }
                #endregion

                _repo.Add(realStateGallery);
                return RedirectToAction("Index",new { realStateId  = realStateGallery.RealStateId});
            }
            //ViewBag.RealStateId = realStateGallery.RealStateId;
            return View(realStateGallery);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateGallery realStateGallery = _repo.Get(id.Value);
            if (realStateGallery == null)
            {
                return HttpNotFound();
            }
            return PartialView(realStateGallery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RealStateGallery realStateGallery, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (Image != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/RealStateImages/Gallery/" + realStateGallery.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/RealStateImages/Gallery/" + realStateGallery.Image));
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(1170, 620, true);
                    image.Resize(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName),
                        Server.MapPath("/Files/RealStateImages/Gallery/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));
                    realStateGallery.Image = newFileName;
                }
                #endregion
                _repo.Update(realStateGallery);
                return RedirectToAction("Index", new { realStateId = realStateGallery.RealStateId });
            }
            return View(realStateGallery);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateGallery realStateGallery = _repo.Get(id.Value);
            if (realStateGallery == null)
            {
                return HttpNotFound();
            }
            return PartialView(realStateGallery);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var gallery = _repo.Get(id);
            var realStateId = gallery.RealStateId;
            _repo.Delete(id);
            return RedirectToAction("Index", new { realStateId });
        }
    }
}