using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SpadCompanyPanel.Core.Models;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class RealStatesRepository : BaseRepository<RealState, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public RealStatesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }


        public RealState GetRealStateWithNavigations(int id)
        {
            return _context.RealStates.Include(p => p.Plans).Where(w => w.Id == id).FirstOrDefault();
        }

        public List<RealState> GetRecentEStates(int count)
        {
            return _context.Set<RealState>().Where(e => e.IsDeleted == false).OrderByDescending(e => e.Id).Take(count).ToList();
        }
    }
}
