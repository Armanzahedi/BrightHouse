using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class FoodsRepository : BaseRepository<Food, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public FoodsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<Food> GetFoods()
        {
            return _context.Foods.Include(p => p.FoodType).Where(p => p.IsDeleted == false).OrderByDescending(a => a.InsertDate).ToList();
        }
        public List<FoodType> GetFoodTypes()
        {
            return _context.FoodTypes.Where(a => a.IsDeleted == false).ToList();
        }
    }
}
