using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
        [Authorize]
        public class PartnersController : Controller
        {
            private readonly PartnersRepository _repo;
            public PartnersController(PartnersRepository repo)
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
            public ActionResult Create(Partner partner, HttpPostedFileBase PartnerImage)
            {
                if (ModelState.IsValid)
                {
                    #region Upload Image
                    if (PartnerImage != null)
                    {
                        // Saving Temp Image
                        var newFileName = Guid.NewGuid() + Path.GetExtension(PartnerImage.FileName);
                        PartnerImage.SaveAs(Server.MapPath("/Files/PartnersImages/Temp/" + newFileName));
                        // Resize Image
                        ImageResizer image = new ImageResizer(400, 200, true);
                        image.Resize(Server.MapPath("/Files/PartnersImages/Temp/" + newFileName),
                            Server.MapPath("/Files/PartnersImages/" + newFileName));

                        // Deleting Temp Image
                        System.IO.File.Delete(Server.MapPath("/Files/PartnersImages/Temp/" + newFileName));

                        partner.Image = newFileName;
                    }
                    #endregion

                    _repo.Add(partner);
                    return RedirectToAction("Index");
                }

                return View(partner);
            }

            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Partner image = _repo.Get(id.Value);
                if (image == null)
                {
                    return HttpNotFound();
                }
                return PartialView(image);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(Partner partner, HttpPostedFileBase PartnerImage)
            {
                if (ModelState.IsValid)
                {
                    #region Upload Image
                    if (PartnerImage != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath("/Files/PartnersImages/" + partner.Image)))
                            System.IO.File.Delete(Server.MapPath("/Files/PartnersImages/" + partner.Image));

                        // Saving Temp Image
                        var newFileName = Guid.NewGuid() + Path.GetExtension(PartnerImage.FileName);
                        PartnerImage.SaveAs(Server.MapPath("/Files/PartnersImages/Temp/" + newFileName));
                        // Resize Image
                        ImageResizer image = new ImageResizer(400, 200, true);
                        image.Resize(Server.MapPath("/Files/PartnersImages/Temp/" + newFileName),
                            Server.MapPath("/Files/PartnersImages/" + newFileName));

                        // Deleting Temp Image
                        System.IO.File.Delete(Server.MapPath("/Files/PartnersImages/Temp/" + newFileName));
                        partner.Image = newFileName;
                    }
                    #endregion

                    _repo.Update(partner);
                    return RedirectToAction("Index");
                }
                return View(partner);
            }
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Partner image = _repo.Get(id.Value);
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
                //    if (System.IO.File.Exists(Server.MapPath("/Files/PartnersImages/" + image.Image)))
                //        System.IO.File.Delete(Server.MapPath("/Files/PartnersImages/" + image.Image));

                //    if (System.IO.File.Exists(Server.MapPath("/Files/PartnersImages/" + image.Image)))
                //        System.IO.File.Delete(Server.MapPath("/Files/PartnersImages/" + image.Image));
                //}
                //#endregion

                _repo.Delete(id);
                return RedirectToAction("Index");
            }
        }
}