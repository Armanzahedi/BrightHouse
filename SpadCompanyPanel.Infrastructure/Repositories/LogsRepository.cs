using SpadCompanyPanel.Core.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class LogsRepository
    {
        private readonly MyDbContext _context;
        public LogsRepository(MyDbContext context)
        {
            _context = context;
        }
        public Log LogEvent(string TableName,int id, string Action)
        {
            var user = GetCurrentUser();
            var log = new Log();
            log.Action = Action;
            log.TableName = TableName;
            log.EntityId = id;
            log.UserName = user.UserName;
            log.ActionDate = DateTime.Now;
            _context.Logs.Add(log);
            _context.SaveChanges();
            return log;
        }
        public User GetCurrentUser()
        {
            var username = HttpContext.Current.User.Identity.GetUserName();
            if (username == null)
                return null;
            var user = _context.Users.FirstOrDefault(u => u.UserName.Trim().ToLower() == username.Trim().ToLower());
            return user;

        }
    }
}
