using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpadCompanyPanel.Core.Models;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class RealStateCommentsRepository : BaseRepository<RealStateComment, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public RealStateCommentsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<RealStateComment> GetRealStateComments(int realStateId)
        {
            return _context.RealStateComments.Where(h => h.RealStateId == realStateId & h.IsDeleted == false).ToList();
        }
        public string GetRealStateName(int realStateId)
        {
            return _context.RealStates.Find(realStateId).Title;
        }
        public RealStateComment DeleteComment(int id)
        {
            var comment = _context.RealStateComments.Find(id);
            var children = _context.RealStateComments.Where(c => c.ParentId == id).ToList();
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
        public void AddComment(RealStateComment comment)
        {
            _context.RealStateComments.Add(comment);
            _context.SaveChanges();
        }
    }
}
