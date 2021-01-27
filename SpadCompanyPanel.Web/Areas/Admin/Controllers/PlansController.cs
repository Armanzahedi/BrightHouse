using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;
namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PlansController : Controller
    {
        private readonly PlansRepository _repo;
        private readonly OptionsRepository _optionsRepo;
        public PlansController(PlansRepository repo,
            OptionsRepository optionsRepo)
        {
            _repo = repo;
            _optionsRepo = optionsRepo;
        }
        public ActionResult Index(int realStateId)
        {
            ViewBag.RealStateName = _repo.GetRealStateName(realStateId);
            ViewBag.RealStateId = realStateId;
            var plans = _repo.GetRealStatePlans(realStateId);
            return View(plans);
        }

        public ActionResult Create(int realStateId)
        {
            ViewBag.RealStateId = realStateId;
            ViewBag.Options = _optionsRepo.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Plan plan,List<int> Options,HttpPostedFileBase PlanImage)
        {
            if (plan.RentPrice == null)
                plan.RentPrice = 0;

            if (ModelState.IsValid)
            {
                #region Upload Files
                if (PlanImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(PlanImage.FileName);
                    PlanImage.SaveAs(Server.MapPath("/Files/PlanFiles/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(370, 270, true);
                    image.Resize(Server.MapPath("/Files/PlanFiles/Temp/" + newFileName),
                        Server.MapPath("/Files/PlanFiles/PlanImages/" + newFileName));
                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/PlanFiles/Temp/" + newFileName));

                    plan.Image = newFileName;
                }
                #endregion
                _repo.Add(plan);
                foreach (var option in Options)
                {
                   _repo.AddOption(plan.Id,option); 
                }
                return RedirectToAction("Index", new { realStateId = plan.RealStateId });
            }
            ViewBag.RealStateId = plan.RealStateId;
            return View(plan);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = _repo.Get(id.Value);
            if (plan == null)
            {
                return HttpNotFound();
            }

            ViewBag.SelectedOptions = _repo.GetPlanOptions(plan.Id);
            ViewBag.Options = _optionsRepo.GetAll();
            return View(plan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Plan plan, List<int> Options, HttpPostedFileBase PlanImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Files
                if (PlanImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/PlanFiles/PlanImages/" + plan.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/PlanFiles/PlanImages/" + plan.Image));

                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(PlanImage.FileName);
                    PlanImage.SaveAs(Server.MapPath("/Files/PlanFiles/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(370, 270, true);
                    image.Resize(Server.MapPath("/Files/PlanFiles/Temp/" + newFileName),
                        Server.MapPath("/Files/PlanFiles/PlanImages/" + newFileName));
                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/PlanFiles/Temp/" + newFileName));

                    plan.Image = newFileName;
                }
                #endregion

                _repo.Update(plan);
                _repo.DeleteOptions(plan.Id);
                foreach (var option in Options)
                {
                    _repo.AddOption(plan.Id, option);
                }
                return RedirectToAction("Index", new { realStateId = plan.RealStateId });
            }
            return View(plan);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = _repo.Get(id.Value);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return PartialView(plan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var realStateId = _repo.Get(id).RealStateId;
            _repo.Delete(id);
            return RedirectToAction("Index", new { realStateId });
        }
    }
}