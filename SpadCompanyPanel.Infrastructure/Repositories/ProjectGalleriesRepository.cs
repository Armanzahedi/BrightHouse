using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class FoodGalleriesRepository : BaseRepository<FoodGallery, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public FoodGalleriesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        //public List<FoodGallery> GetFoodGalleries(int FoodId)
        //{
        //    //return _context.FoodGalleries.Where(h => h.FoodId == FoodId & h.IsDeleted == false).ToList();
        //}
        public string GetFoodName(int FoodId)
        {
            return _context.Foods.Find(FoodId).Title;
        }
    }
}
