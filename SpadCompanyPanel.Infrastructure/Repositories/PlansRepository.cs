using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpadCompanyPanel.Core.Models;


namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class PlansRepository : BaseRepository<Plan, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public PlansRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<Plan> GetRealStatePlans(int realStateId)
        {
            return _context.Plans.Where(h => h.RealStateId == realStateId & h.IsDeleted == false).ToList();
        }
        public string GetRealStateName(int realStateId)
        {
            return _context.RealStates.Find(realStateId).Title;
        }

        public void AddOption(int planId, int optionId)
        {
            var planOption = new PlanOption()
            {
                PlanId = planId,
                OptionId = optionId
            };
            var user = GetCurrentUser();
            planOption.InsertDate = DateTime.Now;
            planOption.InsertUser = user.UserName;
            var ent = planOption.GetType().Name;

            _context.PlanOptions.Add(planOption);
            _context.SaveChanges();
            _logger.LogEvent(planOption.GetType().Name, planOption.Id, "Add");
        }
        public void DeleteOptions(int planId)
        {
            var options = _context.PlanOptions.Where(o => o.IsDeleted == false && o.PlanId == planId).ToList();
            foreach (var option in options)
            {
                var user = GetCurrentUser();
                option.InsertDate = DateTime.Now;
                option.InsertUser = user.UserName;
                var ent = option.GetType().Name;
                option.IsDeleted = true;
                _context.Entry(option).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogEvent(option.GetType().Name, option.Id, "Delete");
            }
        }

        public List<PlanOption> GetPlanOptions(int planId)
        {
            return _context.PlanOptions.Where(o => o.IsDeleted == false && o.PlanId == planId).ToList();
        }
    }
}
