using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
