using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpadCompanyPanel.Core.Models;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class OptionsRepository : BaseRepository<Option, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public OptionsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Option> GetPlanOptions(int planId)
        {
           return _context.PlanOptions.Where(w => w.PlanId == planId && w.IsDeleted == false).Select(s => s.Option).ToList();

            //return _context.Options.Where(w=>)
        }
    }
}
