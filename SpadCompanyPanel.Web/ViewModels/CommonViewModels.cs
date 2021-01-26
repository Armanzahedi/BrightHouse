using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpadCompanyPanel.Core.Models;

namespace SpadCompanyPanel.Web.ViewModels
{
    public class FooterViewModel
    {
        public StaticContentDetail Map { get; set; }
        public StaticContentDetail Phone { get; set; }
        public StaticContentDetail Email { get; set; }
        public StaticContentDetail Address { get; set; }
        public StaticContentDetail Youtube { get; set; }
        public StaticContentDetail Instagram { get; set; }
        public StaticContentDetail Twitter { get; set; }
        public StaticContentDetail Facebook { get; set; }
        public StaticContentDetail Pinterest { get; set; }
    }
    public class ContactUsViewModel
    {
        public StaticContentDetail ContactInfo { get; set; }
        public StaticContentDetail Phone { get; set; }
        public StaticContentDetail Email { get; set; }
        public StaticContentDetail Address { get; set; }
        public StaticContentDetail Youtube { get; set; }
        public StaticContentDetail Instagram { get; set; }
        public StaticContentDetail Twitter { get; set; }
        public StaticContentDetail Facebook { get; set; }
        public StaticContentDetail Pinterest { get; set; }
    }

    public class GalleryPageViewModel
    {
        public List<Gallery> Images { get; set; }
        public List<GalleryVideo> Videos { get; set; }
    }
}