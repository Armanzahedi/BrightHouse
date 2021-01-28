using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class InvoicesRepository : BaseRepository<Invoice, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public InvoicesRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<Invoice> GetInvoices()
        {
            return _context.Invoices.Include(i => i.Customer.User).Include(i=>i.Plan.RealState).OrderByDescending(i=>i.RegisteredDate).ToList();
        }
        public Invoice GetInvoice(int invoiceId)
        {
            return _context.Invoices.Include(i => i.Customer.User).FirstOrDefault(i => i.Id == invoiceId);
        }
        public List<Invoice> GetCustomerInvoices(int customerId)
        {
            return _context.Invoices.Where(i => i.IsDeleted == false && i.CustomerId == customerId).ToList();
        }
    }
}
