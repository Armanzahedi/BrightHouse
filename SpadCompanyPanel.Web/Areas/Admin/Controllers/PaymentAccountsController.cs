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
    public class PaymentAccountsController : Controller
    {
        private readonly PaymentAccountsRepository _repo;
        public PaymentAccountsController(PaymentAccountsRepository repo)
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
        public ActionResult Create(PaymentAccount paymentAccount, HttpPostedFileBase PaymentAccountImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (PaymentAccountImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(PaymentAccountImage.FileName);
                    PaymentAccountImage.SaveAs(Server.MapPath("/Files/PaymentAccountsQrCode/" + newFileName));
                    paymentAccount.QrCode = newFileName;
                }
                #endregion

                _repo.Add(paymentAccount);
                return RedirectToAction("Index");
            }

            return View(paymentAccount);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentAccount image = _repo.Get(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }
            return PartialView(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentAccount paymentAccount, HttpPostedFileBase PaymentAccountImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (PaymentAccountImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/PaymentAccountsQrCode/" + paymentAccount.QrCode)))
                        System.IO.File.Delete(Server.MapPath("/Files/PaymentAccountsQrCode/" + paymentAccount.QrCode));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(PaymentAccountImage.FileName);
                    PaymentAccountImage.SaveAs(Server.MapPath("/Files/PaymentAccountsQrCode/" + newFileName));
                    paymentAccount.QrCode = newFileName;
                }
                #endregion

                _repo.Update(paymentAccount);
                return RedirectToAction("Index");
            }
            return View(paymentAccount);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentAccount image = _repo.Get(id.Value);
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
            //    if (System.IO.File.Exists(Server.MapPath("/Files/PaymentAccountsImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/PaymentAccountsImages/" + image.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/PaymentAccountsImages/" + image.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/PaymentAccountsImages/" + image.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}