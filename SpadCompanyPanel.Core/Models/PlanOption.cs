using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class PlanOption : IBaseEntity
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
