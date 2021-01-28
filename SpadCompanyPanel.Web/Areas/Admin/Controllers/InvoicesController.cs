using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Core.Utility;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly InvoicesRepository _repo;
        private readonly GeoDivisionsRepository _GeoRepo;

        public InvoicesController(InvoicesRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/Invoices
        public ActionResult Index()
        {
            var invoices = _repo.GetInvoices();
            var vm = new List<InvoiceTableViewModel>();
            foreach (var invoice in invoices)
            {
                vm.Add(new InvoiceTableViewModel(invoice));
            }
            return View(vm);
        }
        public bool ConfirmPayment(int id)
        {
            try
            {
                var invoice = _repo.Get(id);
                invoice.PaymentStatus = (int)PaymentStatus.Confirmed;
                _repo.Update(invoice);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = _repo.Get(id.Value);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersianDate = new PersianDateTime(invoice.RegisteredDate).ToString();
            return PartialView(invoice);
        }

        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var invoice = _repo.Get(id);
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}