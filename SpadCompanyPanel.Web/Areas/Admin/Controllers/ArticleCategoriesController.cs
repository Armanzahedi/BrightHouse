using System;
using System.Net;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Repositories;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleCategoriesController : Controller
    {
        private readonly ArticleCategoriesRepository _repo;
        public ArticleCategoriesController(ArticleCategoriesRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/ArticleCategories
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Admin/ArticleCategories/Create
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleCategory articleCategory)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(articleCategory);
                return RedirectToAction("Index");
            }

            return View(articleCategory);
        }

        // GET: Admin/ArticleCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleCategory articleCategory = _repo.Get(id.Value);
            if (articleCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView(articleCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleCategory articleCategory)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(articleCategory);
                return RedirectToAction("Index");
            }
            return View(articleCategory);
        }

        // GET: Admin/ArticleCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleCategory articleCategory = _repo.Get(id.Value);
            if (articleCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView(articleCategory);
        }

        // POST: Admin/ArticleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
