using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Core.Utility;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Web.ViewModels;
using System.Data.Entity;
using SpadCompanyPanel.Infrastructure.Services;
using SpadCompanyPanel.Infrastructure.Dtos.News;

namespace SpadCompanyPanel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly StaticContentDetailsRepository _contentRepo;
        private readonly TestimonialsRepository _testimonialRepo;
        private readonly ContactFormsRepository _contactFormRepo;
        private readonly OurTeamRepository _ourTeamRepo;
        private readonly CertificatesRepository _certificatesRepo;
        private readonly FoodGalleriesRepository _foodGalleriesRepo;
        private readonly RealStatesRepository _stateRepos;
        private readonly GeoDivisionsRepository _geoRepos;
        private readonly PlansRepository _planRepos;
        private readonly CurrenciesRepository _currencyRepo;
        private readonly StaticContentService _staticContentService;
        private readonly NewsService _newsService;
        private readonly PartnersRepository _partnersRepo;
        private readonly NewsRepository _newsRepos;

        public HomeController(StaticContentDetailsRepository contentRepo, TestimonialsRepository testimonialRepo, ContactFormsRepository contactFormRepo, OurTeamRepository ourTeamRepo,
            CertificatesRepository certificatesRepo, FoodGalleriesRepository foodGalleriesRepo, RealStatesRepository stateRepos, GeoDivisionsRepository geoRepos, PlansRepository planRepos,
            CurrenciesRepository currencyRepo, StaticContentService staticContentService, PartnersRepository partnersRepo, NewsRepository newsRepos, NewsService newsService)
        {
            _contentRepo = contentRepo;
            _testimonialRepo = testimonialRepo;
            _contactFormRepo = contactFormRepo;
            _ourTeamRepo = ourTeamRepo;
            _certificatesRepo = certificatesRepo;
            _foodGalleriesRepo = foodGalleriesRepo;
            _stateRepos = stateRepos;
            _geoRepos = geoRepos;
            _planRepos = planRepos;
            _currencyRepo = currencyRepo;
            _staticContentService = staticContentService;
            _partnersRepo = partnersRepo;
            _newsRepos = newsRepos;
            _newsService = newsService;
        }
        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();
            var recentStates = _stateRepos.GetAll().Take(6).ToList();
            var realStateViewModels = new List<RealStateViewModel>();
            var language = LanguageHelper.GetCulture();

            recentStates.ForEach(item =>
            {
                var plan = _planRepos.GetRealStatePlans(item.Id).FirstOrDefault();

                if (plan != null)
                {
                    var realModel = new RealStateViewModel()
                    {
                        Id = item.Id,
                        Area = plan.Area,
                        Bathroom = plan.BathRooms,
                        Bedroom = plan.Rooms,
                        Price = plan.Price,
                        Location = _geoRepos.GetFullLocation(item.GeoDivisionId),
                        Title = language == (int)Language.Farsi ? item.Title : item.EnglishTitle,
                        ShortDescription = language == (int)Language.Farsi ? item.ShortDescription : item.EnglishShortDescription,
                        Image = item.Image
                    };

                    realStateViewModels.Add(realModel);
                }
            });

            model.RecentRealStates = realStateViewModels;

            return View(model);
        }
        public ActionResult Navbar()
        {
            ViewBag.Currencies = _currencyRepo.GetAll();
            //ViewBag.Phone = _contentRepo.GetStaticContentDetail((int) StaticContents.Phone).ShortDescription;
            return PartialView();
        }
        public ActionResult InternalNavbar()
        {
            //ViewBag.Phone = _contentRepo.GetStaticContentDetail((int) StaticContents.Phone).ShortDescription;
            ViewBag.Currencies = _currencyRepo.GetAll();
            return PartialView();
        }
        public ActionResult HomeSlider()
        {
            var sliderContent = _contentRepo.GetContentByTypeId((int)StaticContentTypes.Slider);
            return PartialView(sliderContent);
        }
        public ActionResult CompanyHistory()
        {
            var content = _contentRepo.GetContentByTypeId((int)StaticContentTypes.CompanyHistory).FirstOrDefault();
            return PartialView(content);
        }
        public ActionResult Testimonials()
        {
            var content = _testimonialRepo.GetAll();
            return PartialView(content);
        }
        public ActionResult ContactUsForm()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult ContactUsForm(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                _contactFormRepo.Add(contactForm);
                return RedirectToAction("ContactUsSummary");
            }
            return View(contactForm);
        }
        public ActionResult ContactUsSummary()
        {
            return View();
        }
        public ActionResult PartnersSection()
        {
            var partners = _partnersRepo.GetAll();
            return PartialView(partners);
        }
        [Route("OurTeam")]
        public ActionResult OurTeamPage()
        {
            var ourTeam = _ourTeamRepo.GetAll();
            return View(ourTeam);
        }
        public ActionResult Footer()
        {
            //var footerContent = new FooterViewModel();
            //footerContent.Map = _contentRepo.Get((int) StaticContents.Map);
            //footerContent.Email = _contentRepo.Get((int) StaticContents.Email);
            //footerContent.Address = _contentRepo.Get((int) StaticContents.Address);
            //footerContent.Phone = _contentRepo.Get((int) StaticContents.Phone);
            //footerContent.Youtube = _contentRepo.Get((int) StaticContents.Youtube);
            //footerContent.Instagram = _contentRepo.Get((int) StaticContents.Instagram);
            //footerContent.Twitter = _contentRepo.Get((int) StaticContents.Twitter);
            //footerContent.Pinterest = _contentRepo.Get((int) StaticContents.Pinterest);
            //footerContent.Facebook = _contentRepo.Get((int) StaticContents.Facebook);

            return PartialView();
        }
        [Route("Certificates")]
        public ActionResult Certificates()
        {
            var certificates = _certificatesRepo.GetAll();
            return View(certificates);
        }
        [Route("Foods")]
        public ActionResult Foods()
        {
            var foodGallery = _foodGalleriesRepo.GetAll();
            return View(foodGallery);
        }
        [Route("AboutUs")]
        public ActionResult About()
        {
            var model = _staticContentService.GetAboutUsContext();
            return View(model);
        }
        [Route("ContactUs")]
        public ActionResult Contact()
        {
            ViewBag.ContactUsPage = true;
            var contactUsContent = new ContactUsViewModel();
            contactUsContent.ContactInfo = _contentRepo.Get((int)StaticContents.ContactInfo);
            contactUsContent.Email = _contentRepo.Get((int)StaticContents.Email);
            contactUsContent.Address = _contentRepo.Get((int)StaticContents.Address);
            contactUsContent.Phone = _contentRepo.Get((int)StaticContents.Phone);
            contactUsContent.Youtube = _contentRepo.Get((int)StaticContents.Youtube);
            contactUsContent.Instagram = _contentRepo.Get((int)StaticContents.Instagram);
            contactUsContent.Twitter = _contentRepo.Get((int)StaticContents.Twitter);
            contactUsContent.Pinterest = _contentRepo.Get((int)StaticContents.Pinterest);
            contactUsContent.Facebook = _contentRepo.Get((int)StaticContents.Facebook);
            return View(contactUsContent);
        }
        [Route("News")]
        public ActionResult News()
        {
            var model = _newsService.GetNews();

            return View(model);
        }
        [Route("NewsDetail/{id}")]
        public ActionResult NewsDetail(int id)
        {
            var model = _newsService.GetNewsDetail(id);

            return View(model);
        }

        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                    Path.GetExtension(upload.FileName).ToLower();
                    var vFolderPath = Server.MapPath("/Upload/");
                    if (!Directory.Exists(vFolderPath))
                    {
                        Directory.CreateDirectory(vFolderPath);
                    }
                    vFilePath = Path.Combine(vFolderPath, vFileName);
                    upload.SaveAs(vFilePath);
                    vImagePath = Url.Content("/Upload/" + vFileName);
                    vMessage = "Image was saved correctly";
                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput);
        }


        #region Index Items

        //public ActionResult Contact()
        //{
        //    ViewBag.ContactUsPage = true;
        //    var contactUsContent = new ContactUsViewModel();
        //    contactUsContent.ContactInfo = _contentRepo.Get((int)StaticContents.ContactInfo);
        //    contactUsContent.Email = _contentRepo.Get((int)StaticContents.Email);
        //    contactUsContent.Address = _contentRepo.Get((int)StaticContents.Address);
        //    contactUsContent.Phone = _contentRepo.Get((int)StaticContents.Phone);
        //    contactUsContent.Youtube = _contentRepo.Get((int)StaticContents.Youtube);
        //    contactUsContent.Instagram = _contentRepo.Get((int)StaticContents.Instagram);
        //    contactUsContent.Twitter = _contentRepo.Get((int)StaticContents.Twitter);
        //    contactUsContent.Pinterest = _contentRepo.Get((int)StaticContents.Pinterest);
        //    contactUsContent.Facebook = _contentRepo.Get((int)StaticContents.Facebook);
        //    return View(contactUsContent);
        //}
        #endregion
    }
}