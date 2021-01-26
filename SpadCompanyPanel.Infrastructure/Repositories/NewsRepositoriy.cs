using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace SpadCompanyPanel.Infrastructure.Repositories
{
    public class NewsRepository : BaseRepository<News, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly LogsRepository _logger;
        public NewsRepository(MyDbContext context, LogsRepository logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
        public News GetNews(int id)
        {
            return _context.News.Include(a => a.User).FirstOrDefault(a => a.Id == id);
        }
        public List<News> GetNews()
        {
            return _context.News.Where(a => a.IsDeleted == false).Include(a => a.User).OrderByDescending(a => a.AddedDate).ToList();
        }

        public void AddNews(News News)
        {
            var user = GetCurrentUser();
            News.InsertDate = DateTime.Now;
            News.InsertUser = user.UserName;
            News.AddedDate = DateTime.Now;
            News.UserId = user.Id;
            _context.News.Add(News);
            _context.SaveChanges();
            _logger.LogEvent(News.GetType().Name, News.Id, "Add");
        }

        public List<News> GetTopNews(int? take = null)
        {
            return take != null ? _context.News.Where(a => a.IsDeleted == false).OrderByDescending(a => a.ViewCount).Take(take.Value).ToList() : _context.News.OrderByDescending(a => a.ViewCount).ToList();
        }

        public string GetAuthorRole(string userId)
        {
            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
            var role = _context.Role.FirstOrDefault(r => r.Id == userRole.RoleId);
            return role.RoleNameLocal;
        }

        public int GetNewsCount()
        {
            return _context.News.Count(a => a.IsDeleted == false);
        }
        
        public void UpdateNewsViewCount(int NewsId)
        {
            var News = _context.News.Find(NewsId);
            News.ViewCount++;
            _context.Entry(News).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public List<News> GetLatestNews(int? take = null)
        {
            return take != null ? _context.News.Where(a => a.IsDeleted == false).OrderByDescending(a => a.AddedDate).Take(take.Value).ToList() : _context.News.OrderByDescending(a => a.AddedDate).ToList();
        }

        //public News DeleteNews(int NewsId)
        //{
        //    var News = _context.News.Find(NewsId);
        //    if (News == null)
        //        return null;
        //    var NewsTags = _context.NewsTags.Where(t => t.NewsId == NewsId).ToList();
        //    var articeheadLines = _context.
        //}
    }
}
