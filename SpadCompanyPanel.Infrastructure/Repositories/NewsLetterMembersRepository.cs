using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class NewsLetterMembersRepository : BaseRepository<NewsLetterMember, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public NewsLetterMembersRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public NewsLetterMember AddNewsLetterEmail(string email)
        {
            var model = new NewsLetterMember()
            {
                Email = email,
                InsertDate = DateTime.Now,
                IsDeleted = false
            };
            _context.NewsLetterMembers.Add(model);
            _context.SaveChanges();
            return model;
        }
    }
}
