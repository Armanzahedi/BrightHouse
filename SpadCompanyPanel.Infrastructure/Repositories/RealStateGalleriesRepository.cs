using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpadCompanyPanel.Core.Models;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class RealStateGalleriesRepository : BaseRepository<RealStateGallery, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public RealStateGalleriesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public string GetRealSateName(int FoodId)
        {
            return _context.Foods.Find(FoodId).Title;
        }
        public List<RealStateGallery> GetRealStateGalleries(int realStateId)
        {
            return _context.RealStateGalleries.Where(h => h.RealStateId == realStateId & h.IsDeleted == false).ToList();
        }
        public string GetRealStateName(int realStateId)
        {
            return _context.RealStates.Find(realStateId).Title;
        }
        public RealStateComment DeleteImage(int id)
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
    }
}
