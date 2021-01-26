using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Core.Models;
using System.Net;
using SpadCompanyPanel.Web.ViewModels;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleCommentsController : Controller
    {
        private readonly ArticleCommentsRepository _repo;
        public ArticleCommentsController(ArticleCommentsRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index(int articleId)
        {
            ViewBag.ArticleName = _repo.GetArticleName(articleId);
            ViewBag.ArticleId = articleId;
            var comments = _repo.GetArticleComments(articleId);
            var commentsVm = new List<CommentWithPersianDateViewModel>();
            foreach (var comment in comments)
            {
                var commentVm = new CommentWithPersianDateViewModel(comment);
                commentsVm.Add(commentVm);
            }
            return View(commentsVm);
        }

        public ActionResult Create(int articleId)
        {
            ViewBag.ArticleId = articleId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleComment comment)
        {
            if (ModelState.IsValid)
            {
                comment.AddedDate = DateTime.Now;
                _repo.Add(comment);
                return RedirectToAction("Index", new { articleId = comment.ArticleId });
            }
            ViewBag.ArticleId = comment.ArticleId;
            return View(comment);
        }
        public ActionResult AnswerComment(int articleId,int parentCommentId)
        {
            ViewBag.ArticleId = articleId;
            ViewBag.ParentId = parentCommentId;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerComment(ArticleComment comment)
        {
            var user = _repo.GetCurrentUser();
            comment.Name = user != null? $"{user.FirstName} {user.LastName}" : "ادمین";
            comment.Email = user != null ? user.Email :"-";
            comment.AddedDate = DateTime.Now;
            _repo.Add(comment);
            return RedirectToAction("Index", new { articleId = comment.ArticleId });
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleComment comment = _repo.Get(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleComment comment)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(comment);
                return RedirectToAction("Index", new { articleId = comment.ArticleId });
            }
            return View(comment);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleComment comment = _repo.Get(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }

            var commentVm = new CommentWithPersianDateViewModel(comment);
            return PartialView(commentVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var articleId = _repo.Get(id).ArticleId;
            _repo.DeleteComment(id);
            return RedirectToAction("Index", new { articleId });
        }
    }
}