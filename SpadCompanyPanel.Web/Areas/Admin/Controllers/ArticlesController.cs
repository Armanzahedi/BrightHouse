using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Web.ViewModels;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly ArticlesRepository _repo;
        public ArticlesController(ArticlesRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/Articles
        public ActionResult Index()
        {
            var articles = _repo.GetArticles();
            var articlesListVm = new List<ArticleInfoViewModel>();
            foreach (var article in articles)
            {
                var articleVm = new ArticleInfoViewModel(article);
                articlesListVm.Add(articleVm);
            }
            return View(articlesListVm);
        }
        // GET: Admin/Articles/Create
        public ActionResult Create()
        {
            ViewBag.ArticleCategoryId = new SelectList(_repo.GetArticleCategories(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, HttpPostedFileBase ArticleImage, string Tags)
        {
            if (ModelState.IsValid)
            {

                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    ViewBag.Message = "کاربر وارد کننده پیدا نشد.";
                    return View(article);
                }


                #region Upload Image
                if (ArticleImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(ArticleImage.FileName);
                    ArticleImage.SaveAs(Server.MapPath("/Files/ArticleImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(820, 340, true);
                    image.Resize(Server.MapPath("/Files/ArticleImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ArticleImages/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer(400, 300, true);
                    thumb.Resize(Server.MapPath("/Files/ArticleImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ArticleImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/ArticleImages/Temp/" + newFileName));

                    article.Image = newFileName;
                }
                #endregion

                _repo.AddArticle(article);

                if (!string.IsNullOrEmpty(Tags))
                    _repo.AddArticleTags(article.Id, Tags);

                return RedirectToAction("Index");
            }
            ViewBag.Tags = Tags;
            ViewBag.ArticleCategoryId = new SelectList(_repo.GetArticleCategories(), "Id", "Title", article.ArticleCategoryId);
            return View(article);
        }

        // GET: Admin/Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _repo.GetArticle(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tags = _repo.GetArticleTagsStr(id.Value);
            ViewBag.ArticleCategoryId = new SelectList(_repo.GetArticleCategories(), "Id", "Title", article.ArticleCategoryId);
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article, HttpPostedFileBase ArticleImage, string Tags)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (ArticleImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/ArticleImages/Image/" + article.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/ArticleImages/Image/" + article.Image));

                    if (System.IO.File.Exists(Server.MapPath("/Files/ArticleImages/Thumb/" + article.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/ArticleImages/Thumb/" + article.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(ArticleImage.FileName);
                    ArticleImage.SaveAs(Server.MapPath("/Files/ArticleImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(820, 340, true);
                    image.Resize(Server.MapPath("/Files/ArticleImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ArticleImages/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer(400, 300, true);
                    thumb.Resize(Server.MapPath("/Files/ArticleImages/Temp/" + newFileName),
                        Server.MapPath("/Files/ArticleImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/ArticleImages/Temp/" + newFileName));

                    article.Image = newFileName;
                }
                #endregion

                _repo.Update(article);

                if (!string.IsNullOrEmpty(Tags))
                    _repo.AddArticleTags(article.Id, Tags);

                return RedirectToAction("Index");
            }
            ViewBag.Tags = Tags;
            ViewBag.ArticleCategoryId = new SelectList(_repo.GetArticleCategories(), "Id", "Title", article.ArticleCategoryId);
            return View(article);
        }

        [HttpPost]
        public ActionResult FileUpload()
        {
            var files = HttpContext.Request.Files;
            foreach (var fileName in files)
            {
                HttpPostedFileBase file = Request.Files[fileName.ToString()];
                var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("/Files/ArticleImages/" + newFileName));
                TempData["UploadedFile"] = newFileName;
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        // GET: Admin/Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _repo.GetArticle(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            return PartialView(article);
        }

        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var article = _repo.Get(id);

            //#region Delete Article Image
            //if (article.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/ArticleImages/Image/" + article.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/ArticleImages/Image/" + article.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/ArticleImages/Thumb/" + article.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/ArticleImages/Thumb/" + article.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
