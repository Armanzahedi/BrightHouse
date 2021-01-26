using SpadCompanyPanel.Core.Utility;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;
using SpadCompanyPanel.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SpadCompanyPanel.Infrastructure.Dtos.RealState;
using SpadCompanyPanel.Infrastructure.Services;

namespace SpadCompanyPanel.Web.Controllers
{
    public class RealStateController : Controller
    {
        private readonly RealStatesRepository _realStateRepos;
        private readonly RealStateGalleriesRepository _realStateGalleryRepos;
        private readonly GeoDivisionsRepository _geoDivisionsRepo;
        private readonly OptionsRepository _optionRepos;
        private readonly PlansRepository _planRepos;
        private readonly RealStateService _realStateService;
        private readonly CurrenciesRepository _currencyRepo;
        private readonly int _currentLang;
        private readonly int _currentCurrency;
        public RealStateController(RealStatesRepository realStateRepos, 
            OptionsRepository optionRepos,
            RealStateGalleriesRepository realStateGalleryRepos,
            GeoDivisionsRepository geoDivisionsRepo,
            PlansRepository planRepos,
            GeoDivisionsRepository geoRepos,
            RealStateService realStateService,
            CurrenciesRepository currencyRepo)
        {
            _realStateRepos = realStateRepos;
            _optionRepos = optionRepos;
            _realStateGalleryRepos = realStateGalleryRepos;
            _geoDivisionsRepo = geoDivisionsRepo;
            _planRepos = planRepos;
            _realStateService = realStateService;
            _currencyRepo = currencyRepo;
            _currentLang = LanguageHelper.GetCulture();
            _currentCurrency = CurrencyHelper.GetCurrencyId();
        }
        // GET: RealState
        public ActionResult Index()
        {
            ViewBag.Countries = _geoDivisionsRepo.GetCountries();
            ViewBag.MinPrice = Convert.ToInt64(_realStateService.GetLowestPriceOfPlans());
            ViewBag.MaxPrice = Convert.ToInt64(_realStateService.GetHighestPriceOfPlans());
            var model = new RealStatesViewModel();

            var realStates = _realStateRepos.GetAll();

            var recentStates = realStates.Take(4);

            model.RecentRealStates = recentStates.ToList();


            return View(model);
        }
        public ActionResult StateGrid(RealStateGridViewModel model)
        {
            var realStates = new List<RealStateInfoDto>();
            realStates = _realStateService.GetRealStateGrid(model.countryId,model.stateId,model.cityId,model.realStateType,model.planType,model.roomNo,model.bathRoomNo,model.priceFrom,model.priceTo);
            if (string.IsNullOrEmpty(model.sort) == false)
            {
                switch (model.sort)
                {
                    case "1":
                        realStates = realStates.OrderByDescending(p => p.RealStateType == (int)RealStateType.Apartment).ToList();
                        break;
                    case "2":
                        realStates = realStates.OrderByDescending(p => p.RealStateType == (int)RealStateType.Villa).ToList();
                        break;
                    case "3":
                        realStates = realStates.OrderByDescending(p => p.RealStateType == (int)RealStateType.Office).ToList();
                        break;
                    case "4":
                        realStates = realStates.OrderByDescending(p => p.RealStateType == (int)RealStateType.Commercial).ToList();
                        break;
                    case "5":
                        realStates = realStates.OrderByDescending(p => p.RealStateType == (int)RealStateType.Land).ToList();
                        break;
                }
            }
            var count = realStates.Count;
            var skip = model.pageNumber * model.take - model.take;
            int pageCount = (int)Math.Ceiling((double)count / model.take);
            ViewBag.PageCount = pageCount;
            ViewBag.CurrentPage = model.pageNumber;

            realStates = realStates.Skip(skip).Take(model.take).ToList();
            return View(realStates);
        }
        [Route("RealState/Details/{id}")]
        public ActionResult Details(int id)
        {
            var realState = _realStateRepos.GetRealStateWithNavigations(id);
            var realStateCity = _geoDivisionsRepo.Get(realState.GeoDivisionId);
            var realStateState = _geoDivisionsRepo.Get(realStateCity.ParentId.Value);
            var realStateCountry = _geoDivisionsRepo.GetGeoDivisionParent(realStateState.ParentId.Value);
            var planId = 0;

            var model = new RealStateViewModel();

            if (realState.Plans != null)
            {
                var language = LanguageHelper.GetCulture();
                planId = realState.Plans.FirstOrDefault().Id;

                var plans = _planRepos.GetRealStatePlans(realState.Id);
                var plan = plans.FirstOrDefault();

                var options = _optionRepos.GetPlanOptions(planId);

                model.Description = realState.Description;
                model.Region = realState.Region;
                model.Title = realState.Title;
                model.StateType = realState.Type == (int)RealStateType.Apartment ? "آپارتمان" : (realState.Type == (int)RealStateType.Villa ? "ویلا" : (realState.Type == (int)RealStateType.Office ? "اداری" :
                (realState.Type == (int)RealStateType.Commercial ? "تجاری" : "ویلا")));
                model.Options = options;
                model.City = realStateCity.Title;
                model.Country = realStateCountry.Title;
                model.Id = realState.Id;
                model.Area = plan.Area;
                model.Bathroom = plan.BathRooms;
                model.Bedroom = plan.Rooms;
                model.Price = plan.Price;
                model.Location = _geoDivisionsRepo.GetFullLocation(realState.GeoDivisionId);
                model.Title = language == (int)Language.Farsi ? realState.Title : realState.EnglishTitle;
                model.ShortDescription = language == (int)Language.Farsi ? realState.ShortDescription : realState.EnglishShortDescription;
                model.Image = realState.Image;

                model.Plans = realState.Plans;
                var vm = new List<PlanWithOptionViewModel>();
                foreach (var item in realState.Plans)
                {
                    var OPT = _optionRepos.GetPlanOptions(item.Id);
                    vm.Add(new PlanWithOptionViewModel()
                    {
                        Plan = item,
                        Options = OPT
                    });
                }

                ViewBag.PlanWithOptions = vm;
            }

            ViewBag.RealStateGallery = _realStateGalleryRepos.GetRealStateGalleries(id);
            return View(model);
        }
        public ActionResult RecentStates(RealStatesViewModel model)
        {
            return View(model);
        }
        public string GetGeoDivisionChildren(int? geoDivisionId = null)
        {
            if (geoDivisionId != null)
            {
                var geoDivisions = _geoDivisionsRepo.GetGeoDivisionChildren(geoDivisionId.Value);
                var obj = geoDivisions.Select(g => new { id = g.Id, title = _currentLang == (int)Language.Farsi ? g.Title : g.LatinTitle });
                return JsonConvert.SerializeObject(obj);
            }
            return "";
        }
    }
}