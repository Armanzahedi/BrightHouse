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
        private readonly NewsLetterMembersRepository _newsLetterMember;
        private readonly GeoDivisionsRepository _geoDivisionRepo;
        private readonly GeoDivisionService _geoDivisionService;
        private readonly NewsRepository _newsRepos;
        private readonly RealStateService _realStateService;
        private readonly BlogService _blogService;

        public HomeController(StaticContentDetailsRepository contentRepo, TestimonialsRepository testimonialRepo, ContactFormsRepository contactFormRepo, OurTeamRepository ourTeamRepo,
            CertificatesRepository certificatesRepo, FoodGalleriesRepository foodGalleriesRepo, RealStatesRepository stateRepos, GeoDivisionsRepository geoRepos, PlansRepository planRepos,
            CurrenciesRepository currencyRepo, StaticContentService staticContentService, PartnersRepository partnersRepo,
            NewsLetterMembersRepository newsLetterMember,
            GeoDivisionsRepository geoDivisionRepo, GeoDivisionService geoDivisionService, NewsService newsService, NewsRepository newsRepos, RealStateService realStateService, BlogService blogService)
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
            _newsLetterMember = newsLetterMember;
            _geoDivisionRepo = geoDivisionRepo;
            _geoDivisionService = geoDivisionService;
            _newsRepos = newsRepos;
            _newsService = newsService;
            _realStateService = realStateService;
            _blogService = blogService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RealStateGuideFilter()
        {
            ViewBag.Countries = _geoDivisionService.GetCountries();
            return PartialView();
        }
        public ActionResult LuxurtVillasSection()
        {
            var luxuryVillas = _realStateService.GetByType((int)RealStateType.Villa, 3);
            return PartialView(luxuryVillas);
        }
        public ActionResult RecentRealStates()
        {
            var realStates = _realStateService.TakeRealStates(6);
            return PartialView(realStates);
        }
        public ActionResult LatestArticles()
        {
            var articles = _blogService.GetLatestArticlesInfo(6);
            return PartialView(articles);
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
            var lang = LanguageHelper.GetCulture();
            ViewBag.ContactUsPage = true;
            var contactUsContent = new ContactUsViewModel();
            contactUsContent.ContactInfo = _contentRepo.Get((int)StaticContents.ContactInfo);
            contactUsContent.Email = _contentRepo.Get((int)StaticContents.Email);
            contactUsContent.Address = _contentRepo.Get(lang == (int)Language.Farsi ? (int)StaticContents.Address : (int)StaticContents.AddressEnglish);
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

        [HttpPost]
        public ActionResult SubmitNewsLetterEmail(string newsLetterEmail)
        {
            if (ModelState.IsValid)
            {
                var model = new NewsLetterMember()
                {
                    Email = newsLetterEmail
                };
                _newsLetterMember.Add(model);
            }
            return RedirectToAction("Index", "Home");
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