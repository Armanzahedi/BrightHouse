using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Dtos.StaticContent
{
    public class AboutUsDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public AboutUsItemDto Item1 { get; set; }
        public AboutUsItemDto Item2 { get; set; }
        public AboutUsItemDto Item3 { get; set; }
        public AboutUsItemDto Item4 { get; set; }
    }

    public class AboutUsItemDto
    {
        public string Title { get; set; }
        public string  ShortDescription { get; set; }
    }
}
