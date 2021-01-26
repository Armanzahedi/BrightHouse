using System;
using System.Net;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Repositories;
using System.Web;
using System.IO;
using SpadCompanyPanel.Infrastructure.Helpers;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class CertificatesController : Controller
    {
        private readonly CertificatesRepository _repo;
        public CertificatesController(CertificatesRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Certificate certificate, HttpPostedFileBase CertificateImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (CertificateImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(CertificateImage.FileName);
                    CertificateImage.SaveAs(Server.MapPath("/Files/CertificatesImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(800, 600, true);
                    image.Resize(Server.MapPath("/Files/CertificatesImages/Temp/" + newFileName),
                        Server.MapPath("/Files/CertificatesImages/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/CertificatesImages/Temp/" + newFileName));
                    certificate.Image = newFileName;
                }
                #endregion

                _repo.Add(certificate);
                return RedirectToAction("Index");
            }

            return View(certificate);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate image = _repo.Get(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }
            return PartialView(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Certificate certificate, HttpPostedFileBase CertificateImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (CertificateImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/CertificatesImages/" + certificate.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/CertificatesImages/" + certificate.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(CertificateImage.FileName);
                    CertificateImage.SaveAs(Server.MapPath("/Files/CertificatesImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(800, 600, true);
                    image.Resize(Server.MapPath("/Files/CertificatesImages/Temp/" + newFileName),
                        Server.MapPath("/Files/CertificatesImages/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/CertificatesImages/Temp/" + newFileName));
                    certificate.Image = newFileName;
                }
                #endregion

                _repo.Update(certificate);
                return RedirectToAction("Index");
            }
            return View(certificate);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate image = _repo.Get(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }
            return PartialView(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var image = _repo.Get(id);

            //#region Delete Image
            //if (image.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/CertificatesImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/CertificatesImages/" + image.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/CertificatesImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/CertificatesImages/" + image.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}