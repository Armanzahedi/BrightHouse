using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpadCompanyPanel.Core.Models
{
    public class UserRole : IdentityUserRole
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
