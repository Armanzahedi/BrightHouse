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
    public class GeoDivisionsController : Controller
    {
        private readonly GeoDivisionsRepository _geoDivisionRepo;
        public GeoDivisionsController(GeoDivisionsRepository geoDivisionRepo)
        {
            _geoDivisionRepo = geoDivisionRepo;
        }
        // GET: Admin/GeoDivisionTypes
        public ActionResult Index(int? parentId = null)
        {
            if (parentId != null)
            {
                var parent = _geoDivisionRepo.Get(parentId.Value);
                ViewBag.ParentId = parentId;
                if (parent.ParentId != null)
                {
                    ViewBag.PrevParent = parent.ParentId;
                }

            }
            return View(_geoDivisionRepo.GetGeoDivisions(parentId));
        }
        public ActionResult Create(int? parentId = null)
        {
            ViewBag.ParentId = parentId;
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GeoDivision geoDivision)
        {
            geoDivision.GeoDivisionType = 0;
            geoDivision.IsArchived = 0;
            if (ModelState.IsValid)
            {
                if (geoDivision.ParentId != null)
                {
                    var parent = _geoDivisionRepo.Get(geoDivision.ParentId.Value);
                    geoDivision.GeoDivisionType = parent.GeoDivisionType + 1;
                }
                _geoDivisionRepo.Add(geoDivision);
                return RedirectToAction("Index",new {parentId = geoDivision.ParentId});
            }

            return View(geoDivision);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeoDivision geoDivision = _geoDivisionRepo.Get(id.Value);
            if (geoDivision == null)
            {
                return HttpNotFound();
            }
            return PartialView(geoDivision);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GeoDivision geoDivision)
        {
            if (ModelState.IsValid)
            {
                _geoDivisionRepo.Update(geoDivision);
                return RedirectToAction("Index", new { parentId = geoDivision.ParentId });
            }
            return View(geoDivision);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeoDivision geoDivision = _geoDivisionRepo.Get(id.Value);
            if (geoDivision == null)
            {
                return HttpNotFound();
            }
            return PartialView(geoDivision);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var geoDivision = _geoDivisionRepo.Get(id);
            var parentId = geoDivision.ParentId;
            _geoDivisionRepo.Delete(id);
            return RedirectToAction("Index", new { parentId });
        }
    }
}