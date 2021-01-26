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
    public class CurrenciesController : Controller
    {
        private readonly CurrenciesRepository _repo;
        public CurrenciesController(CurrenciesRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/Currencies
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Admin/Currencies/Create
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Currency currency)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(currency);
                return RedirectToAction("Index");
            }

            return View(currency);
        }

        // GET: Admin/Currencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = _repo.Get(id.Value);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return PartialView(currency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Currency currency)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(currency);
                return RedirectToAction("Index");
            }
            return View(currency);
        }

        // GET: Admin/Currencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = _repo.Get(id.Value);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return PartialView(currency);
        }

        // POST: Admin/Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}