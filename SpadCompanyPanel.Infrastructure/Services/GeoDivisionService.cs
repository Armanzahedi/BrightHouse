using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Services
{
   public class GeoDivisionService
    {
        private readonly MyDbContext _context;
        private readonly int _currentLang;
        private readonly int _currentCurrency;
        public GeoDivisionService(MyDbContext context)
        {
            _context = context;
            _currentCurrency = CurrencyHelper.GetCurrencyId();
            _currentLang = LanguageHelper.GetCulture();
        }
        public List<GeoDivision> GetCountries()
        {
            var countries = _context.GeoDivisions.Where(g => g.IsDeleted == false && g.ParentId == null).ToList();
            foreach (var item in countries)
                if (_currentLang != 1)
                    item.Title = item.LatinTitle;
            return countries;
        }
    }
}
