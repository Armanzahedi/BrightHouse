using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class Permission
    {
        public int Id { get; set; }
        [MaxLength(300)]
        public string Title { get; set; }
        [MaxLength(300)]
        public string Name { get; set; }
        //[MaxLength(600)]
        //public string PermissionNameLocal { get; set; }
        public int DisplayPriority { get; set; }
        [MaxLength(300)]
        public string ControllerName { get; set; }
        [MaxLength(300)]
        public string ActionName { get; set; }
        public int? ParentId { get; set; }
        public virtual Permission Parent { get; set; }
        public virtual ICollection<Permission> Children { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
