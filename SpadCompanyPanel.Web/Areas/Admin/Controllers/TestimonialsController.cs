using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;

namespace SpadCompanyPanel.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class TestimonialsController : Controller
    {
        private readonly TestimonialsRepository _repo;
        public TestimonialsController(TestimonialsRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Testimonial testimonial, HttpPostedFileBase TestimonialImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (TestimonialImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/TestimonialImages/" + testimonial.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/TestimonialImages/" + testimonial.Image));
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(TestimonialImage.FileName);
                    TestimonialImage.SaveAs(Server.MapPath("/Files/TestimonialImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(200, 200);
                    image.Resize(Server.MapPath("/Files/TestimonialImages/Temp/" + newFileName),
                        Server.MapPath("/Files/TestimonialImages/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/TestimonialImages/Temp/" + newFileName));

                    testimonial.Image = newFileName;
                }
                #endregion
                _repo.Add(testimonial);
                return RedirectToAction("Index");
            }

            return View(testimonial);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = _repo.Get(id.Value);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return PartialView(testimonial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Testimonial testimonial, HttpPostedFileBase TestimonialImage)
        {
            if (ModelState.IsValid)
            {
                #region Upload Image
                if (TestimonialImage != null)
                {
                    // Saving Temp Image
                    var newFileName = Guid.NewGuid() + Path.GetExtension(TestimonialImage.FileName);
                    TestimonialImage.SaveAs(Server.MapPath("/Files/TestimonialImages/Temp/" + newFileName));
                    // Resize Image
                    ImageResizer image = new ImageResizer(200, 200);
                    image.Resize(Server.MapPath("/Files/TestimonialImages/Temp/" + newFileName),
                        Server.MapPath("/Files/TestimonialImages/" + newFileName));

                    // Deleting Temp Image
                    System.IO.File.Delete(Server.MapPath("/Files/TestimonialImages/Temp/" + newFileName));

                    testimonial.Image = newFileName;
                }
                #endregion
                _repo.Update(testimonial);
                return RedirectToAction("Index");
            }
            return View(testimonial);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = _repo.Get(id.Value);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return PartialView(testimonial);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}