using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FOODSTATION.Models.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }


        [Required]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }
        [Phone]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "صلاحية المستخدم")]
        public string Role { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تاكيد كلمة السر")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string RegistrationInValid { get; set; }
    }
}