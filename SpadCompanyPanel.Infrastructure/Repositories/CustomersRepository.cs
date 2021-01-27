using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Helpers;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class CustomersRepository : BaseRepository<Customer, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public CustomersRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Customer> GetCustomerTable()
        {
            return _context.Customers.Where(c => c.IsDeleted == false).Include(c => c.User).ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Include(c=>c.User).FirstOrDefault(c => c.Id == id);
        }
        public Customer GetCurrentCustomer()
        {
            var currentUserId = CheckPermission.GetCurrentUserId();
            return _context.Customers.Include(c => c.User).Include(c=>c.GeoDivision).FirstOrDefault(c => c.UserId == currentUserId);
        }
    }
}
