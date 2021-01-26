using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class ArticleCommentsRepository : BaseRepository<ArticleComment, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ArticleCommentsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<ArticleComment> GetArticleComments(int articleId)
        {
            return _context.ArticleComments.Where(h => h.ArticleId == articleId & h.IsDeleted == false).ToList();
        }
        public string GetArticleName(int articleId)
        {
            return _context.Articles.Find(articleId).Title;
        }
        public ArticleComment DeleteComment(int id)
        {
            var comment = _context.ArticleComments.Find(id);
            var children = _context.ArticleComments.Where(c=>c.ParentId == id).ToList();
            foreach (var child in children)
            {
                child.IsDeleted = true;
                _context.Entry(child).State = EntityState.Modified;
                _context.SaveChanges();
            }
            comment.IsDeleted = true;
            _context.Entry(comment).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogEvent(comment.GetType().Name, comment.Id, "Delete");
            return comment;
        }
    }
}
