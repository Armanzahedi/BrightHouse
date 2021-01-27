using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Core.Utility;
using SpadCompanyPanel.Infrastructure.Dtos.News;
using SpadCompanyPanel.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Services
{
    public class NewsService
    {
        private readonly MyDbContext _context;
        private readonly int _currentLang;
        private readonly int _currentCurrency;
        public NewsService(MyDbContext context)
        {
            _context = context;
            _currentCurrency = CurrencyHelper.GetCurrencyId();
            _currentLang = LanguageHelper.GetCulture();
        }


        public List<NewsDto> GetNews()
        {
            var news = _context.News.Where(w => !w.IsDeleted).OrderByDescending(p => p.Id).ToList();

            var newsList = new List<NewsDto>();

            news.ForEach(item =>
            {
                var newsItem = new NewsDto
                {
                    Id = item.Id,
                    Image = item.Image,
                    Title = item.Title
                };

                newsList.Add(newsItem);
            });

            return newsList;
        }

        public NewsDetailDto GetNewsDetail(int id)
        {
            var news = _context.News.Where(w => w.Id == id && !w.IsDeleted).FirstOrDefault() ?? new News();
            var similar = _context.News.Where(w => w.Id != id && !w.IsDeleted).OrderByDescending(p => p.Id).Take(4).Select(s => new NewsDto
            {
                Id = s.Id,
                Image = s.Image,
                Title = s.Title,
            }).ToList();

            var newsItem = new NewsDetailDto
            {
                Id = news.Id,
                Image = news.Image,
                Title = news.Title,
                ShortDescription = news.ShortDescription,
                Description = news.Description,
                SimilarNews = similar
            };

            return newsItem;
        }
    }
}
