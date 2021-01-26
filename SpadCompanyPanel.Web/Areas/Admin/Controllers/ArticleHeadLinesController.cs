using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Core.Models;
using System.Net;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleHeadLinesController : Controller
    {
        private readonly ArticleHeadLinesRepository _repo;
        public ArticleHeadLinesController(ArticleHeadLinesRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index(int articleId)
        {
            ViewBag.ArticleName = _repo.GetArticleName(articleId);
            ViewBag.ArticleId = articleId;
            return View(_repo.GetArticleHeadLines(articleId));
        }

        public ActionResult Create(int articleId)
        {
            ViewBag.ArticleId = articleId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleHeadLine headLine)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(headLine);
                return RedirectToAction("Index",new { articleId = headLine.ArticleId });
            }
            ViewBag.ArticleId = headLine.ArticleId;
            return View(headLine);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleHeadLine headLine = _repo.Get(id.Value);
            if (headLine == null)
            {
                return HttpNotFound();
            }
            return View(headLine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleHeadLine headLine)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(headLine);
                return RedirectToAction("Index", new { articleId = headLine.ArticleId});
            }
            return View(headLine);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleHeadLine headLine = _repo.Get(id.Value);
            if (headLine == null)
            {
                return HttpNotFound();
            }
            return PartialView(headLine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var articleId = _repo.Get(id).ArticleId;
            _repo.Delete(id);
            return RedirectToAction("Index",new { articleId });
        }
    }
}