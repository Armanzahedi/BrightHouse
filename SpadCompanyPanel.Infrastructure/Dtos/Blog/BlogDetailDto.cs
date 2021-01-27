using SpadCompanyPanel.Core.Models;
using SpadCompanyPanel.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Dtos.Blog
{
    public class BlogDetailDto
    {
        private List<BlogCategoryModel> _categories;

        public List<BlogCategoryModel> Categories
        {
            get { return _categories ?? (_categories = new List<BlogCategoryModel>()); }
            set { _categories = value; }
        }

        private List<BlogViewModel> _recentBlogs;

        public List<BlogViewModel> RecentBlogs
        {
            get { return _recentBlogs ?? (_recentBlogs = new List<BlogViewModel>()); }
            set { _recentBlogs = value; }
        }

        public BlogDetailDto()
        {

        }
        public BlogDetailDto(Article article,int _currentLang)
        {
            this.Id = article.Id;
            this.Image = article.Image;
            this.Author = article.User != null ? $"{article.User.FirstName} {article.User.LastName}" : "-";
            this.PersianDate = article.AddedDate != null ? new PersianDateTime(article.AddedDate.Value).ToString("dddd d MMMM yyyy") : "-";
            this.Title = _currentLang == (int)Language.Farsi ? article.Title : article.EnglishTitle;
            this.ShortDescription = _currentLang == (int)Language.Farsi ? article.ShortDescription : article.EnglishShortDescription;
            this.Description = _currentLang == (int)Language.Farsi ? article.Description : article.EnglishDescription;                
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string PersianDate { get; set; }
        public List<ArticleTag> Tags { get; set; }
        //public List<ArticleCommentViewModel> ArticleComments { get; set; }
        //public CommentFormViewModel CommentForm { get; set; }
    }
}
