using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Dtos.Blog;
using SpadCompanyPanel.Infrastructure.Helpers;
using SpadCompanyPanel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Services
{
    public class BlogService
    {
        private readonly MyDbContext _context;
        private readonly int _currentLang;
        private readonly int _currentCurrency;
        public BlogService(MyDbContext context)
        {
            _context = context;
            _currentCurrency = CurrencyHelper.GetCurrencyId();
            _currentLang = LanguageHelper.GetCulture();
        }

        public BlogDto GetBlogArticle(int? categoryId = null)
        {
            var blogDto = new BlogDto();
            var repos = new ArticlesRepository(_context, new LogsRepository(_context));
            var blogs = new List<BlogViewModel>();
            if (categoryId == null)
            {
                blogs = repos.GetArticles().Select(s=>new BlogViewModel(s)).ToList();
            }
            else
            {
                var category = repos.GetCategory(categoryId.Value);
                if (category != null)
                {
                    //ViewBag.BreadCrumb = category.Title;
                    blogs = repos.GetArticlesByCategory(categoryId.Value).Select(s => new BlogViewModel(s)).ToList();
                }
            }

            var categories = _context.ArticleCategories.Where(w => !w.IsDeleted).Select(s => new BlogCategoryModel { Id = s.Id, Title = s.Title }).ToList();

            blogDto.Blogs.AddRange(blogs);
            blogDto.Categories.AddRange(categories);

            return blogDto;
        }
    }
}
