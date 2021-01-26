using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Web.ViewModels;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class RealStateCommentsController : Controller
    {
        private readonly RealStateCommentsRepository _repo;
        public RealStateCommentsController(RealStateCommentsRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index(int realStateId)
        {
            ViewBag.RealStateName = _repo.GetRealStateName(realStateId);
            ViewBag.RealStateId = realStateId;
            var comments = _repo.GetRealStateComments(realStateId);
            var commentsVm = new List<RealStateCommentWithPersianDateViewModel>();
            foreach (var comment in comments)
            {
                var commentVm = new RealStateCommentWithPersianDateViewModel(comment);
                commentsVm.Add(commentVm);
            }
            return View(commentsVm);
        }

        public ActionResult Create(int realStateId)
        {
            ViewBag.RealStateId = realStateId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RealStateComment comment)
        {
            if (ModelState.IsValid)
            {
                comment.AddedDate = DateTime.Now;
                _repo.Add(comment);
                return RedirectToAction("Index", new { realStateId = comment.RealStateId });
            }
            ViewBag.RealStateId = comment.RealStateId;
            return View(comment);
        }
        public ActionResult AnswerComment(int realStateId, int parentCommentId)
        {
            ViewBag.RealStateId = realStateId;
            ViewBag.ParentId = parentCommentId;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerComment(RealStateComment comment)
        {
            var user = _repo.GetCurrentUser();
            comment.Name = user != null ? $"{user.FirstName} {user.LastName}" : "ادمین";
            comment.Email = user != null ? user.Email : "-";
            comment.AddedDate = DateTime.Now;
            _repo.Add(comment);
            return RedirectToAction("Index", new { realStateId = comment.RealStateId });
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateComment comment = _repo.Get(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RealStateComment comment)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(comment);
                return RedirectToAction("Index", new { realStateId = comment.RealStateId });
            }
            return View(comment);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealStateComment comment = _repo.Get(id.Value);
            if (comment == null)
            {
                return HttpNotFound();
            }

            var commentVm = new RealStateCommentWithPersianDateViewModel(comment);
            return PartialView(commentVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var realStateId = _repo.Get(id).RealStateId;
            _repo.DeleteComment(id);
            return RedirectToAction("Index", new { realStateId });
        }
    }
}