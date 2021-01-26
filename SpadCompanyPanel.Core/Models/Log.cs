using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class Log
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string TableName { get; set; }
        public int EntityId { get; set; }
        [MaxLength(100)]
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
