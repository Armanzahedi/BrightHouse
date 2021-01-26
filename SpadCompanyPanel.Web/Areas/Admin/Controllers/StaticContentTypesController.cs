using System;
using System.Net;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Repositories;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class StaticContentTypesController : Controller
    {
        private readonly StaticContentTypesRepository _repo;
        public StaticContentTypesController(StaticContentTypesRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/StaticContentTypes
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Admin/StaticContentTypes/Create
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaticContentType staticContentType)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(staticContentType);
                return RedirectToAction("Index");
            }

            return View(staticContentType);
        }

        // GET: Admin/StaticContentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticContentType staticContentType = _repo.Get(id.Value);
            if (staticContentType == null)
            {
                return HttpNotFound();
            }
            return PartialView(staticContentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StaticContentType staticContentType)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(staticContentType);
                return RedirectToAction("Index");
            }
            return View(staticContentType);
        }

        // GET: Admin/StaticContentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaticContentType staticContentType = _repo.Get(id.Value);
            if (staticContentType == null)
            {
                return HttpNotFound();
            }
            return PartialView(staticContentType);
        }

        // POST: Admin/StaticContentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
