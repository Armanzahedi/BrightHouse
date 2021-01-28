using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SpadCompanyPanel.Core.Models;

namespace SpadCompanyPanel.Web.ViewModels
{
    public class RealStateCommentWithPersianDateViewModel : RealStateComment
    {
        public RealStateCommentWithPersianDateViewModel(RealStateComment comment)
        {
            this.Comment = comment;
            this.PersianDate = comment.AddedDate != null ? new PersianDateTime(comment.AddedDate.Value).ToString() : "-";
        }
        public RealStateComment Comment { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public string PersianDate { get; set; }


    }

    public class RealStateViewModel : RealState
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Bathroom { get; set; }
        public string Bedroom { get; set; }
        public string StateType { get; set; }
        public float Price { get; set; }

        private List<Option> _options;

        public List<Option> Options
        {
            get { return _options ?? (_options=new List<Option>()); }
            set { _options = value; }
        }
    }

    public class RealStatesViewModel
    {
        public RealStatesViewModel()
        {
        }

        private List<RealStateViewModel> _realStates;
        public List<RealStateViewModel> RealStates
        {
            get { return _realStates ?? (_realStates = new List<RealStateViewModel>()); }
            set { _realStates = value; }
        }

        private List<RealState> _recentRealStates;
        public List<RealState> RecentRealStates
        {
            get { return _recentRealStates ?? (_recentRealStates = new List<RealState>()); }
            set { _recentRealStates = value; }
        }

    }

    public class PlanWithOptionViewModel
    {
        public Plan Plan { get; set; }
        public List<Option> Options { get; set; }
    }
    public class RealStateGridViewModel
    {
        public int? countryId { get; set; }
        public int? stateId { get; set; }
        public int? cityId { get; set; }
        public int? realStateType { get; set; }
        public int? planType { get; set; }
        public string roomNo { get; set; }
        public string bathRoomNo { get; set; }
        public long? priceFrom { get; set; }
        public long? priceTo { get; set; }
        public int pageNumber { get; set; }
        public int take { get; set; }
        public string sort { get; set; }
    }
}