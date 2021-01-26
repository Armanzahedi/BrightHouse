using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpadCompanyPanel.Web.ViewModels
{
    public class HomeIndexViewModel
    {

        private List<RealStateViewModel> _recentRealStates;
        public List<RealStateViewModel> RecentRealStates
        {
            get { return _recentRealStates ?? (_recentRealStates = new List<RealStateViewModel>()); }
            set { _recentRealStates = value; }
        }
    }
}