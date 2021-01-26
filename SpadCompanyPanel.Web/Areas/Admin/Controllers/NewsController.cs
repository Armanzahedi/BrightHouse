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
    public class NewsController : Controller
    {
        private readonly NewsRepository _repo;
        public NewsController(NewsRepository repo)
        {
            _repo = repo;
        }
        // GET: Admin/News
        public ActionResult Index()
        {
            var NewsList = _repo.GetNews();
            var NewsListVm = new List<NewsInfoViewModel>();
            foreach (var news in NewsList)
            {
                var NewsVm = new NewsInfoViewModel(news);
                NewsListVm.Add(NewsVm);
            }
            return View(NewsListVm);
        }
        // GET: Admin/News/Create
        public ActionResult Create()
        {
            //ViewBag.NewsCategoryId = new SelectList(_repo.GetNewsCategories(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News News, HttpPostedFileBase NewsImage, string Tags)
        {
            if (ModelState.IsValid)
            {

                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    ViewBag.Message = "کاربر وارد کننده پیدا نشد.";
                    return View(News);
                }


                #region Upload Image
                if (NewsImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(NewsImage.FileName);
                    NewsImage.SaveAs(Server.MapPath("/Files/NewsImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(820, 340, true);
                    image.Resize(Server.MapPath("/Files/NewsImages/Temp/" + newFileName),
                        Server.MapPath("/Files/NewsImages/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer(400, 300, true);
                    thumb.Resize(Server.MapPath("/Files/NewsImages/Temp/" + newFileName),
                        Server.MapPath("/Files/NewsImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/NewsImages/Temp/" + newFileName));

                    News.Image = newFileName;
                }
                #endregion

                _repo.AddNews(News);

                

                return RedirectToAction("Index");
            }
            ViewBag.Tags = Tags;
            //ViewBag.NewsCategoryId = new SelectList(_repo.GetNewsCategories(), "Id", "Title", News.NewsCategoryId);
            return View(News);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News News = _repo.GetNews(id.Value);
            if (News == null)
            {
                return HttpNotFound();
            }
            //ViewBag.NewsCategoryId = new SelectList(_repo.GetNewsCategories(), "Id", "Title", News.NewsCategoryId);
            return View(News);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News News, HttpPostedFileBase NewsImage, string Tags)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (NewsImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/NewsImages/Image/" + News.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/NewsImages/Image/" + News.Image));

                    if (System.IO.File.Exists(Server.MapPath("/Files/NewsImages/Thumb/" + News.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/NewsImages/Thumb/" + News.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(NewsImage.FileName);
                    NewsImage.SaveAs(Server.MapPath("/Files/NewsImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(820, 340, true);
                    image.Resize(Server.MapPath("/Files/NewsImages/Temp/" + newFileName),
                        Server.MapPath("/Files/NewsImages/Image/" + newFileName));

                    ImageResizer thumb = new ImageResizer(400, 300, true);
                    thumb.Resize(Server.MapPath("/Files/NewsImages/Temp/" + newFileName),
                        Server.MapPath("/Files/NewsImages/Thumb/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/NewsImages/Temp/" + newFileName));

                    News.Image = newFileName;
                }
                #endregion

                _repo.Update(News);


                return RedirectToAction("Index");
            }
            ViewBag.Tags = Tags;
            //ViewBag.NewsCategoryId = new SelectList(_repo.GetNewsCategories(), "Id", "Title", News.NewsCategoryId);
            return View(News);
        }

        [HttpPost]
        public ActionResult FileUpload()
        {
            var files = HttpContext.Request.Files;
            foreach (var fileName in files)
            {
                HttpPostedFileBase file = Request.Files[fileName.ToString()];
                var newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("/Files/NewsImages/" + newFileName));
                TempData["UploadedFile"] = newFileName;
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News News = _repo.GetNews(id.Value);
            if (News == null)
            {
                return HttpNotFound();
            }
            return PartialView(News);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var News = _repo.Get(id);

            //#region Delete News Image
            //if (News.Image != null)
            //{
            //    if (System.IO.File.Exists(Server.MapPath("/Files/NewsImages/Image/" + News.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/NewsImages/Image/" + News.Image));

            //    if (System.IO.File.Exists(Server.MapPath("/Files/NewsImages/Thumb/" + News.Image)))
            //        System.IO.File.Delete(Server.MapPath("/Files/NewsImages/Thumb/" + News.Image));
            //}
            //#endregion

            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
