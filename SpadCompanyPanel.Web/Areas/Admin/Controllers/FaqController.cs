using System;
using System.Net;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Repositories;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class FaqController : Controller
    {
        private readonly FaqRepository _repo;
        public FaqController(FaqRepository repo)
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
        public ActionResult Create(Faq faq)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(faq);
                return RedirectToAction("Index");
            }

            return View(faq);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = _repo.Get(id.Value);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return PartialView(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Faq faq)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(faq);
                return RedirectToAction("Index");
            }
            return View(faq);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = _repo.Get(id.Value);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return PartialView(faq);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}