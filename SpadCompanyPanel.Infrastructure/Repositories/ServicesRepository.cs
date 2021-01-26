using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class ServicesRepository : BaseRepository<Service, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public ServicesRepository(MyDbContext context, LogsRepository logger) : base(context,logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Service> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            return service;
        }
    }
}
