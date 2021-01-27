using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class RealStatesController : Controller
    {
        private readonly RealStatesRepository _realStateRepo;
        private readonly GeoDivisionsRepository _geoDivisionRepo;
        public RealStatesController(GeoDivisionsRepository geoDivisionRepo,
            RealStatesRepository realStateRepo)
        {
            _geoDivisionRepo = geoDivisionRepo;
            _realStateRepo = realStateRepo;
        }
        // GET: Admin/RealStates
        public ActionResult Index()
        {
            var realStates = _realStateRepo.GetAll();
            return View(realStates);
        }
        public string GetGeoDivisionChildren(int geoDivisionId)
        {
            var geoDivisions = _geoDivisionRepo.GetGeoDivisionChildren(geoDivisionId);
            var obj = geoDivisions.Select(g=>new { id = g.Id,title = g.Title});
            return JsonConvert.SerializeObject(obj);
        }
        // GET: Admin/RealStates/Create
        public ActionResult Create()
        {
            ViewBag.Countries = _geoDivisionRepo.GetCountries();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RealState realState, HttpPostedFileBase RealStateImage, HttpPostedFileBase VideoImage, HttpPostedFileBase Video)
        {
            realState.LastViewDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                #region Upload Files
                if (RealStateImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(RealStateImage.FileName);
                    RealStateImage.SaveAs(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer big = new ImageResizer(2000, 2000, true);
                    big.Resize(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName),
                        Server.MapPath("/Files/RealStateImages/Big/" + newFileName));

                    ImageResizer image = new ImageResizer(370, 270, true);
                    image.Resize(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName),
                        Server.MapPath("/Files/RealStateImages/" + newFileName));
                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));

                    realState.Image = newFileName;
                }
                if (VideoImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(VideoImage.FileName);
                    VideoImage.SaveAs(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(870, 500, true);
                    image.Resize(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName),
                        Server.MapPath("/Files/RealStateImages/VideoImages/" + newFileName));
                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));

                    realState.VideoThumbnail = newFileName;
                }
                if (Video != null)
                {
                    var newFileName = Guid.NewGuid() + Path.GetExtension(Video.FileName);
                    Video.SaveAs(Server.MapPath("/Files/RealStateImages/Videos/" + newFileName));
                    realState.VideoStr = newFileName;
                }
                #endregion

                _realStateRepo.Add(realState);
                return RedirectToAction("Index");
            }
            return View(realState);
        }

        // GET: Admin/RealStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealState realState = _realStateRepo.Get(id.Value);
            if (realState == null)
            {
                return HttpNotFound();
            }

            var selectedCity = _geoDivisionRepo.Get(realState.GeoDivisionId);
            var selectedState = _geoDivisionRepo.GetGeoDivisionParent(selectedCity.ParentId.Value);
            var selectedCountry = _geoDivisionRepo.GetGeoDivisionParent(selectedState.ParentId.Value);
            ViewBag.SelectedState = selectedState.Id;
            ViewBag.SelectedCountry = selectedCountry.Id;
            ViewBag.States = _geoDivisionRepo.GetGeoDivisionChildren(selectedCountry.Id);
            ViewBag.Cities = _geoDivisionRepo.GetGeoDivisionChildren(selectedState.Id);
            ViewBag.Countries = _geoDivisionRepo.GetCountries();

            return View(realState);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RealState realState, HttpPostedFileBase RealStateImage, HttpPostedFileBase VideoImage, HttpPostedFileBase Video)
        {
            if (ModelState.IsValid)
            {
                #region Upload Files
                if (RealStateImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/RealStateImages/" + realState.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/RealStateImages/" + realState.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(RealStateImage.FileName);
                    RealStateImage.SaveAs(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));

                    // Resize Image
                    ImageResizer big = new ImageResizer(2000, 2000, true);
                    big.Resize(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName),
                        Server.MapPath("/Files/RealStateImages/Big/" + newFileName));

                    ImageResizer image = new ImageResizer(370, 270, true);
                    image.Resize(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName),
                        Server.MapPath("/Files/RealStateImages/" + newFileName));
                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));

                    realState.Image = newFileName;
                }
                if (VideoImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/RealStateImages/VideoImages/" + realState.VideoThumbnail)))
                        System.IO.File.Delete(Server.MapPath("/Files/RealStateImages/VideoImages/" + realState.VideoThumbnail));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(VideoImage.FileName);
                    VideoImage.SaveAs(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));
                    // Resize Image

                    ImageResizer image = new ImageResizer(870, 500, true);
                    image.Resize(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName),
                        Server.MapPath("/Files/RealStateImages/VideoImages/" + newFileName));
                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/RealStateImages/Temp/" + newFileName));

                    realState.VideoThumbnail = newFileName;
                }
                if (Video != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/RealStateImages/Videos/" + realState.VideoStr)))
                        System.IO.File.Delete(Server.MapPath("/Files/RealStateImages/Videos/" + realState.VideoStr));

                    var newFileName = Guid.NewGuid() + Path.GetExtension(Video.FileName);
                    Video.SaveAs(Server.MapPath("/Files/RealStateImages/Videos/" + newFileName));
                    realState.VideoStr = newFileName;
                }
                #endregion

                _realStateRepo.Update(realState);
                return RedirectToAction("Index");
            }
            ViewBag.GeoDivisionId = new SelectList(_geoDivisionRepo.GetAll(), "Id", "Title", realState.GeoDivisionId);
            return View(realState);
        }

        // GET: Admin/RealStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealState realState = _realStateRepo.Get(id.Value);
            if (realState == null)
            {
                return HttpNotFound();
            }
            return PartialView(realState);
        }

        // POST: Admin/RealStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _realStateRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}