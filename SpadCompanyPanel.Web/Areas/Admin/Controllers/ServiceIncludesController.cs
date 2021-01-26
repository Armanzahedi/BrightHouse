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
    public class ServiceIncludesController : Controller
    {
        private readonly ServiceIncludesRepository _repo;
        public ServiceIncludesController(ServiceIncludesRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index(int serviceId)
        {
            ViewBag.ServiceName = _repo.GetServiceName(serviceId);
            ViewBag.ServiceId = serviceId;
            return View(_repo.GetServiceIncludes(serviceId));
        }

        public ActionResult Create(int serviceId)
        {
            ViewBag.ServiceId = serviceId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceInclude serviceInclude)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(serviceInclude);
                return RedirectToAction("Index", new { serviceId = serviceInclude.ServiceId });
            }
            ViewBag.ServiceId = serviceInclude.ServiceId;
            return View(serviceInclude);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceInclude serviceInclude = _repo.Get(id.Value);
            if (serviceInclude == null)
            {
                return HttpNotFound();
            }
            return View(serviceInclude);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceInclude serviceInclude)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(serviceInclude);
                return RedirectToAction("Index", new { serviceId = serviceInclude.ServiceId });
            }
            return View(serviceInclude);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceInclude serviceInclude = _repo.Get(id.Value);
            if (serviceInclude == null)
            {
                return HttpNotFound();
            }
            return PartialView(serviceInclude);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var serviceId = _repo.Get(id).ServiceId;
            _repo.Delete(id);
            return RedirectToAction("Index", new { serviceId });
        }
    }
}