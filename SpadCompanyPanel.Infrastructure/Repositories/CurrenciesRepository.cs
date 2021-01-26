using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpadCompanyPanel.Core.Models;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class CurrenciesRepository : BaseRepository<Currency, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public CurrenciesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

    }
}
