using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Core.Utility;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class GeoDivisionsRepository : BaseRepository<GeoDivision, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public GeoDivisionsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<GeoDivision> GetGeoDivisions(int? parentId = null)
        {
            return _context.GeoDivisions.Where(g => g.IsDeleted == false && g.ParentId == parentId).ToList();
        }
        public List<GeoDivision> GetCountries()
        {
            return _context.GeoDivisions.Where(g => g.IsDeleted == false && g.ParentId == null).ToList();
        }
        public List<GeoDivision> GetStates()
        {
            return _context.GeoDivisions.Where(g => g.IsDeleted == false && g.GeoDivisionType == 1).ToList();
        }
        public List<GeoDivision> GetCities()
        {
            return _context.GeoDivisions.Where(g => g.IsDeleted == false && g.GeoDivisionType == 2).ToList();
        }
        public List<GeoDivision> GetGeoDivisionChildren(int parent)
        {
            return _context.GeoDivisions.Where(g => g.IsDeleted == false && g.ParentId == parent).ToList();
        }
        public GeoDivision GetGeoDivisionParent(int child)
        {
            return _context.GeoDivisions.FirstOrDefault(g => g.IsDeleted == false && g.Id == child);
        }
        public List<GeoDivision> GetGeoDivisionsByType(int type)
        {
            return _context.GeoDivisions.Where(g => g.IsDeleted == false && g.GeoDivisionType == type).ToList();
        }
        public string GetFullLocation(int lang, int child)
        {
            var geoList = new List<string>();
            var geo = new GeoDivision();
            int? id = child;

            do
            {
                geo = _context.GeoDivisions.FirstOrDefault(g => g.IsDeleted == false && g.Id == id);
                if (lang == (int)Language.Farsi)
                    geoList.Add(geo.Title);
                else
                    geoList.Add(geo.LatinTitle);

                id = geo.ParentId;

            } while (id != null);

            geoList.Reverse();
            return string.Join(", ", geoList);
        }
    }
}
