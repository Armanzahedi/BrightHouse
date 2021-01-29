using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Core.Utility;
using SpadCompanyPanel.Infrastructure.Dtos.RealState;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;

namespace SpadCompanyPanel.Infrastructure.Services
{
    public class RealStateService
    {
        private readonly MyDbContext _context;
        private readonly int _currentLang;
        private readonly int _currentCurrency;
        public RealStateService(MyDbContext context)
        {
            _context = context;
            _currentCurrency = CurrencyHelper.GetCurrencyId();
            _currentLang = LanguageHelper.GetCulture();
        }
        public float GetLowestPriceOfPlans()
        {
            float price = 0;
            var plan = _context.Plans.Where(p => p.IsDeleted == false && p.RealState.IsDeleted == false).OrderBy(p => p.Price).FirstOrDefault();
            if (plan != null)
            {
                price = plan.Price;
                price = CurrencyHelper.ExchangeAmount(plan.Price, _currentCurrency);
            }
            return price;
        }
        public float GetHighestPriceOfPlans()
        {
            float price = 100;
            var plan = _context.Plans.Where(p => p.IsDeleted == false && p.RealState.IsDeleted == false).OrderByDescending(p => p.Price).FirstOrDefault();
            if (plan != null)
            {
                price = plan.Price;
                price = CurrencyHelper.ExchangeAmount(plan.Price, _currentCurrency);
            }
            return price;
        }
        public Plan GetRealStatePlanWithLowestPrice(int realStateId)
        {
            var plan = new Plan();
            if (_context.Plans.Any(p => p.RealStateId == realStateId && p.IsDeleted == false))
            {
                var planWithLowestPrice = _context.Plans
                    .Where(p => p.RealStateId == realStateId && p.IsDeleted == false).OrderBy(p => p.Price).FirstOrDefault();
                if (planWithLowestPrice != null)
                    plan = planWithLowestPrice;
            }

            return plan;
        }
        public float GetRealStateLowestPrice(int realStateId)
        {
            float price = 0;
            var plan = GetRealStatePlanWithLowestPrice(realStateId);
            if (plan != null)
                price = plan.Price;

            var priceBasedOnCurrency = CurrencyHelper.ExchangeAmount(price, _currentCurrency);
            return priceBasedOnCurrency;
        }
        public string GetLocationStr(int child)
        {
            var geoList = new List<string>();
            var geo = new GeoDivision();
            int? id = child;
            do
            {
                geo = _context.GeoDivisions.FirstOrDefault(g => g.IsDeleted == false && g.Id == id);
                if (geo != null)
                {
                    geoList.Add(_currentLang == (int)Language.Farsi ? geo.Title : geo.LatinTitle);
                    id = geo.ParentId;
                }

            } while (id != null);

            geoList.Reverse();
            return string.Join(", ", geoList);
        }

        public RealStateInfoDto CreateRealStateInfo(RealState realState)
        {
            var plan = GetRealStatePlanWithLowestPrice(realState.Id);
            var info = new RealStateInfoDto()
            {
                Id = realState.Id,
                Title = _currentLang == 1 ? realState.Title : realState.EnglishTitle,
                ShortDescription = _currentLang == 1 ? realState.ShortDescription : realState.EnglishShortDescription,
                Area = plan.Area,
                Rooms = plan.Rooms,
                Bathrooms = plan.BathRooms,
                MinPrice = CurrencyHelper.ExchangeAmount(plan.Price, _currentCurrency),
                Location = GetLocationStr(realState.GeoDivisionId),
                RealStateType = realState.Type,
                Image = realState.Image
            };
            return info;
        }
        public RealStateInfoDto GetRealStateInfo(int realStateId)
        {
            var realState = _context.RealStates.Find(realStateId);
            var info = CreateRealStateInfo(realState);
            return info;
        }

        public int GetRealStateStateId(int realStateId)
        {
            var realState = _context.RealStates.Find(realStateId);
            var city = _context.GeoDivisions.Find(realState.GeoDivisionId);
            var state = _context.GeoDivisions.FirstOrDefault(s => s.Id == city.ParentId);
            return state.Id;
        }
        public int GetRealStateCountryId(int realStateId)
        {
            var stateId = GetRealStateStateId(realStateId);
            var state = _context.GeoDivisions.Find(stateId);
            var country = _context.GeoDivisions.FirstOrDefault(s => s.Id == state.ParentId);
            return country.Id;
        }
        public List<RealStateInfoDto> GetByType(int realStateType, int? take)
        {
            var query = _context.RealStates.Where(r => r.IsDeleted == false && r.Type == realStateType);
            if (take != null)
                query.Take(take.Value);

            var realStates = query.ToList();
            var realStateWithInfo = new List<RealStateInfoDto>();
            foreach (var item in realStates)
                realStateWithInfo.Add(CreateRealStateInfo(item));

            return realStateWithInfo;
        }
        public List<RealStateInfoDto> TakeRealStates(int take)
        {
            var realStates = _context.RealStates.Where(r => r.IsDeleted == false).Take(take).ToList();
            var realStatesInfo = new List<RealStateInfoDto>();
            foreach (var item in realStates)
                realStatesInfo.Add(CreateRealStateInfo(item));

            return realStatesInfo;
        }
        public List<RealStateInfoDto> GetRealStateGrid(int? countryId, int? stateId, int? cityId, int? realStateType, int? planType, string roomNo = null, string bathRoomNo = null, float? fromPrice = null, float? toPrice = null)
        {
            var realStatesInfoList = new List<RealStateInfoDto>();
            var realStateQuery = _context.RealStates.Where(p => p.IsDeleted == false);

            if (countryId != null)
                realStateQuery = realStateQuery.Where(p => p.GeoDivision.Parent.ParentId == countryId);

            if (stateId != null)
                realStateQuery = realStateQuery.Where(p => p.GeoDivision.ParentId == stateId);

            if (cityId != null)
                realStateQuery = realStateQuery.Where(p => p.GeoDivisionId == cityId);

            if (realStateType != null)
                realStateQuery = realStateQuery.Where(p => p.Type == realStateType);
            if (planType != null)
                realStateQuery = realStateQuery.Where(r => r.Plans.Any(p => p.PlanType == planType));

            if (roomNo != null)
                realStateQuery = realStateQuery.Where(r => r.Plans.Any(p => p.Rooms == roomNo));

            if (bathRoomNo != null)
                realStateQuery = realStateQuery.Where(r => r.Plans.Any(p => p.BathRooms == bathRoomNo));

            if (fromPrice != null)
            {
                var fromPriceEuro = CurrencyHelper.GetDefaultAmount(fromPrice.Value);
                realStateQuery = realStateQuery.Where(r => r.Plans.Any(p => p.Price >= fromPriceEuro));
            }
            if (toPrice != null)
            {
                var toPriceEuro = CurrencyHelper.GetDefaultAmount(toPrice.Value);
                realStateQuery = realStateQuery.Where(r => r.Plans.Any(p => p.Price <= toPriceEuro));
            }

            var realStatesList = realStateQuery.ToList();

            foreach (var realState in realStatesList)
                realStatesInfoList.Add(CreateRealStateInfo(realState));

            return realStatesInfoList;
        }

        public RealStateDetailDto GetRealStateDetail(int id)
        {
            var model = new RealStateDetailDto();

            var lang = LanguageHelper.GetCulture();

            var _realStateRepos = new RealStatesRepository(_context, new LogsRepository(_context));
            var _geoDivisionsRepo = new GeoDivisionsRepository(_context, new LogsRepository(_context));
            var _planRepos = new PlansRepository(_context, new LogsRepository(_context));
            var _optionRepos = new OptionsRepository(_context, new LogsRepository(_context));
            var _realStateGalleryRepos = new RealStateGalleriesRepository(_context, new LogsRepository(_context));
            var _commentRepos = new RealStateCommentsRepository(_context, new LogsRepository(_context));

            var realState = _realStateRepos.GetRealStateWithNavigations(id);
            var realStateCity = _geoDivisionsRepo.Get(realState.GeoDivisionId);
            var realStateState = _geoDivisionsRepo.Get(realStateCity.ParentId.Value);
            var realStateCountry = _geoDivisionsRepo.GetGeoDivisionParent(realStateState.ParentId.Value);
            //var planId = 0;

            if (realState.Plans != null)
            {
                //planId = realState.Plans.FirstOrDefault().Id;

                var plans = _planRepos.GetRealStatePlans(realState.Id);
                var plan = plans.FirstOrDefault();

                //var options = _optionRepos.GetPlanOptions(planId);

                model.Description = lang == (int)Language.Farsi ? realState.Description : realState.EnglishDescription;
                model.Region = lang == (int)Language.Farsi ? realState.Region : realState.EnglishRegion;
                model.Type = realState.Type;
                //model.Options = options;
                model.City = realStateCity.Title;
                model.Country = realStateCountry.Title;
                model.Id = realState.Id;
                model.Area = plan.Area;
                model.Bathroom = plan.BathRooms;
                model.Bedroom = plan.Rooms;

                var price = CurrencyHelper.ExchangeAmount(plan.Price, _currentCurrency);
                var symbol = CurrencyHelper.GetCurrencyUnit();

                model.Price = price;
                model.PriceSymbol = symbol;
                model.Address = _geoDivisionsRepo.GetFullLocation(lang,realState.GeoDivisionId);
                model.Title = lang == (int)Language.Farsi ? realState.Title : realState.EnglishTitle;
                model.ShortDescription = lang == (int)Language.Farsi ? realState.ShortDescription : realState.EnglishShortDescription;
                model.Image = realState.Image;
                model.VideoStr = realState.VideoStr;
                model.Plans = realState.Plans;
                model.Location = realState.Location;
                var vm = new List<PlanWithOptionDto>();

                foreach (var item in realState.Plans)
                {
                    var OPT = _optionRepos.GetPlanOptions(item.Id);
                    vm.Add(new PlanWithOptionDto()
                    {
                        Plan = item,
                        Options = OPT
                    });
                }

                model.PlanWithOptions = vm;
            }

            model.RealStateGalleriesList = _realStateGalleryRepos.GetRealStateGalleries(id);

            var comments = _commentRepos.GetRealStateComments(id);
            model.RealStateCommentList.AddRange(comments);

            return model;
        }
    }
}
