using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpadCompanyPanel.Core.Models
{
    public class UserMetadata
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نیست")]
        public string Email { get; set; }
        [Display(Name = "تلفن همراه")]
        public string PhoneNumber { get; set; }

    }
    [MetadataType(typeof(UserMetadata))]
    public class User : IdentityUser
    {
        public string Avatar { get; set; }
        [MaxLength(300)]    
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FirstName { get; set; }
        [MaxLength(300)]
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string LastName { get; set; }
        [Display(Name = "اطلاعات")]
        [DataType(DataType.MultilineText)]
        public string Information { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
