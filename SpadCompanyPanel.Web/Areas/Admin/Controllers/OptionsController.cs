using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Repositories;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class OptionsController : Controller
    {
        private readonly OptionsRepository _repo;
        public OptionsController(OptionsRepository repo)
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
        public ActionResult Create(Option options)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(options);
                return RedirectToAction("Index");
            }

            return View(options);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option options = _repo.Get(id.Value);
            if (options == null)
            {
                return HttpNotFound();
            }
            return PartialView(options);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Option options)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(options);
                return RedirectToAction("Index");
            }
            return View(options);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option options = _repo.Get(id.Value);
            if (options == null)
            {
                return HttpNotFound();
            }
            return PartialView(options);
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