using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Dtos.Blog
{
    public class BlogDto
    {
        private List<BlogViewModel> _blogs;

        public List<BlogViewModel> Blogs
        {
            get { return _blogs??(_blogs = new List<BlogViewModel>()); }
            set { _blogs = value; }
        }

        private List<BlogViewModel> _recentBlogs;

        public List<BlogViewModel> RecentBlogs
        {
            get { return _recentBlogs ?? (_recentBlogs = new List<BlogViewModel>()); }
            set { _recentBlogs = value; }
        }

        private List<BlogTagModel> _tags;

        public List<BlogTagModel> Tags
        {
            get { return _tags ?? (_tags = new List<BlogTagModel>()); }
            set { _tags = value; }
        }

        private List<BlogCategoryModel> _categories;

        public List<BlogCategoryModel> Categories
        {
            get { return _categories??(_categories=new List<BlogCategoryModel>()); }
            set { _categories = value; }
        }
    }
    public class BlogViewModel
    {
        public BlogViewModel()
        {
        }

        public BlogViewModel(Article article)
        {
            this.Id = article.Id;
            this.Title = article.Title;
            this.ShortDescription = article.ShortDescription;
            this.Author = article.User != null ? $"{article.User.FirstName} {article.User.LastName}" : "-";
            this.Image = article.Image;
            this.AuthorAvatar = article.User.Avatar ?? "user-avatar.png";
            this.PersianDate = article.AddedDate != null ? new PersianDateTime(article.AddedDate.Value).ToString("d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public int CommentsCount { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public string PersianDate { get; set; }
        public string Author { get; set; }
        public string AuthorAvatar { get; set; }
        public string Role { get; set; }
    }

    public class BlogCategoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArticleCount { get; set; }

    }
    public class BlogTagModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

    }
}