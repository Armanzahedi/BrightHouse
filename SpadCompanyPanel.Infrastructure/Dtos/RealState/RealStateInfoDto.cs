using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Infrastructure.Dtos.RealState
{
    public class RealStateInfoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public int RealStateType { get; set; }
        public string Area { get; set; }
        public string Rooms { get; set; }
        public string Bathrooms { get; set; }
        public string Location { get; set; }
        public float MinPrice { get; set; }
    }

    public class RealStateDetailDto: Core.Models.RealState
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Bathroom { get; set; }
        public string Bedroom { get; set; }
        public string StateType { get; set; }
        public float Price { get; set; }
        public string PriceSymbol { get; set; }
        public int Type { get; set; }

        public RealStateInfoDto Info { get; set; }

        private List<Option> _options;
        public List<Option> Options
        {
            get { return _options ?? (_options = new List<Option>()); }
            set { _options = value; }
        }

        private List<PlanWithOptionDto> _planWithOptions;
        public List<PlanWithOptionDto> PlanWithOptions
        {
            get { return _planWithOptions ?? (_planWithOptions = new List<PlanWithOptionDto>()); }
            set { _planWithOptions = value; }
        }

        private List<RealStateGallery> _realStateGalleries;
        public List<RealStateGallery> RealStateGalleriesList
        {
            get { return _realStateGalleries ?? (_realStateGalleries = new List<RealStateGallery>()); }
            set { _realStateGalleries = value; }
        }
    }

    public class PlanWithOptionDto
    {
        public Plan Plan { get; set; }
        public List<Option> Options { get; set; }
    }
}
