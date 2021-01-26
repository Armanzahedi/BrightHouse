using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadCompanyPanel.Core.Models
{
    public class RoleMetadata
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "نام محلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RoleNameLocal { get; set; }

    }
    [MetadataType(typeof(RoleMetadata))]
    public class Role : IdentityRole
    {
        [MaxLength(300)]
        public string RoleNameLocal { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
