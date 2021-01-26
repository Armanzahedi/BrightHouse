using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class ServiceIncludesRepository : BaseRepository<ServiceInclude, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ServiceIncludesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<ServiceInclude> GetServiceIncludes(int serviceId)
        {
            return _context.ServiceIncludes.Where(h => h.ServiceId == serviceId & h.IsDeleted == false).ToList();
        }
        public string GetServiceName(int serviceId)
        {
            return _context.Services.Find(serviceId).Title;
        }
    }
}