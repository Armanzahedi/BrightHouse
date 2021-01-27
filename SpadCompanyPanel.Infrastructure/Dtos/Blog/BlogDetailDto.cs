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
    }
}
