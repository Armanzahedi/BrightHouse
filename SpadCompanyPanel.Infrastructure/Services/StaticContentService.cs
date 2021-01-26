﻿using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Core.Utility;
using SpadCompanyPanel.Infrastructure.Dtos.StaticContent;
using SpadCompanyPanel.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Services
{
    public class StaticContentService
    {
        private readonly MyDbContext _context;
        private readonly int _currentLang;
        private readonly int _currentCurrency;
        public StaticContentService(MyDbContext context)
        {
            _context = context;
            _currentCurrency = CurrencyHelper.GetCurrencyId();
            _currentLang = LanguageHelper.GetCulture();
        }


        public AboutUsDto GetAboutUsContext()
        {

            var aboutUs = _context.StaticContentDetails
                .Where(w => w.StaticContentTypeId == (_currentLang == (int)Language.Farsi ? (int)StaticContentTypes.AboutUs : (int)StaticContentTypes.AboutUsEnglish) && !w.IsDeleted).FirstOrDefault() ?? new Core.Models.StaticContentDetail();

            var phone = _context.StaticContentDetails.Where(w => w.StaticContentTypeId == (int)StaticContentTypes.Phone && !w.IsDeleted).Select(s => s.Title).FirstOrDefault();

            var items = _context.StaticContentDetails
                .Where(w => w.StaticContentTypeId == (_currentLang == (int)Language.Farsi ? (int)StaticContentTypes.AboutUs : (int)StaticContentTypes.AboutUsEnglish) && !w.IsDeleted).ToList();

            var item1 = items.FirstOrDefault(p => p.Id == (int)StaticContentDetails.AboutUsItem1) ?? new StaticContentDetail();
            var item2 = items.FirstOrDefault(p => p.Id == (int)StaticContentDetails.AboutUsItem2) ?? new StaticContentDetail();
            var item3 = items.FirstOrDefault(p => p.Id == (int)StaticContentDetails.AboutUsItem3) ?? new StaticContentDetail();
            var item4 = items.FirstOrDefault(p => p.Id == (int)StaticContentDetails.AboutUsItem4) ?? new StaticContentDetail();

            return new AboutUsDto
            {
                Description = aboutUs.Description,
                Image = aboutUs.Image,
                Phone = phone,
                Title = aboutUs.Title,
                ShortDescription = aboutUs.ShortDescription,
                Item1 = new AboutUsItemDto { Title = item1.Title, ShortDescription = item1.ShortDescription },
                Item2 = new AboutUsItemDto { Title = item2.Title, ShortDescription = item2.ShortDescription },
                Item3 = new AboutUsItemDto { Title = item3.Title, ShortDescription = item3.ShortDescription },
                Item4 = new AboutUsItemDto { Title = item4.Title, ShortDescription = item4.ShortDescription }
            };
        }
    }
}
