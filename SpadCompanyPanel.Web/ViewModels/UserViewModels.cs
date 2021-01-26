using SpadCompanyPanel.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpadCompanyPanel.Web.ViewModels
{
    public class UserPartialViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string UserAvatar { get; set; }
    }
    public class AddUserViewModel
    {
        public User User { get; set; }
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل 6 کارکتر باشد", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
         ErrorMessage = "رمز عبور باید بیشتر از 6 کارکتر و حداقل شامل یک حرف بزرگ یک حرف کوچک یک عدد و یک کارکتر خاص باشد.")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "عدم تطابق رمز عبور و تکرار رمز عبور")]
        public string ConfirmPassword { get; set; }
    }
    public class EditUserViewModel
    {
        public User User { get; set; }
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل 6 کارکتر باشد", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
         ErrorMessage = "پسورد باید حداقل 6 کارکتر و شامل یک حرف بزرگ یک حرف کوچک یک عدد و یک کارکتر خاص باشد.")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [Compare("Password", ErrorMessage = "عدم تطابق رمز عبور جدید و تکرار رمز عبور جدید")]
        public string ConfirmPassword { get; set; }
    }
    public class ResetMyPasswordViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل 6 کارکتر باشد", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "پسورد باید حداقل 6 کارکتر و شامل یک حرف بزرگ یک حرف کوچک یک عدد و یک کارکتر خاص باشد.")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [Compare("Password", ErrorMessage = "عدم تطابق رمز عبور جدید و تکرار رمز عبور جدید")]
        public string ConfirmPassword { get; set; }
    }
}