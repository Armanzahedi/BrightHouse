using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class ArticleHeadLinesRepository : BaseRepository<ArticleHeadLine, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ArticleHeadLinesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<ArticleHeadLine> GetArticleHeadLines(int articleId)
        {
            return _context.ArticleHeadLines.Where(h => h.ArticleId == articleId & h.IsDeleted == false).ToList();
        }
        public string GetArticleName(int articleId)
        {
            return _context.Articles.Find(articleId).Title;
        }
    }
}
