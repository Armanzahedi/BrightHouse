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
}
