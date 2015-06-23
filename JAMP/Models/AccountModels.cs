using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Added Namespaces
using JAMP.DAL;


namespace JAMP.Models
{
    public class AccountModels
    {
        public class LocalPasswordModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

     
            public Employee emp { get; set; }
        }

        public class LoginModel
        {
            [Required]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
               ErrorMessage = "Email is is not valid.")]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class LoginModelpasswordRecovery
        {
            [Required]
            [Display(Name = "Email")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
        }

        public class ChangeEmail
        {
            [Required]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
              ErrorMessage = "Email is is not valid.")]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Current Email")]
            public string OldEmail { get; set; }

            [Required]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
              ErrorMessage = "Email is is not valid.")]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "New Email")]
            public string NewEmail { get; set; }

            [DataType(DataType.EmailAddress)]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
              ErrorMessage = "Email is is not valid.")]
            [Display(Name = "Confirm new Email")]
            [Compare("NewEmail", ErrorMessage = "The new Email and confirmation Email do not match.")]
            public string ConfirmEmail { get; set; }

          
            public Employee emp { get; set; }
        }

        public class EmployeeRegisterModel
        {
            [Required]
            [Display(Name = "First name")]
            public string UserName { get; set; }
            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }


            //select roles from list
            //[Required]
            [Display(Name = "Role")]
            public string SelectedRole { get; set; }

            [Required]
            [Display(Name = "Telephone")]
            public string Contacts { get; set; }

            [Required]
            [Display(Name = "Email")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
               ErrorMessage = "Email is is not valid.")]
            [DataType(DataType.EmailAddress)]
            public string EmployeeEmail { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class BusinessRegisterModel
        {
            [Required(ErrorMessage = "Business name Required")]
            [Display(Name = "Business Name")]
            public string BusinessName { get; set; }

            [Required]
            [Display(Name = "Business Street Name")]
            public string BusinessStreet { get; set; }

            [Required]
            [Display(Name = "Business City")]
            public string BusinessCity { get; set; }

            [Required]
            [Display(Name = "Business Postal Code")]
            public string BusinessPostalCode { get; set; }

            [Required]
            [Display(Name = "Business Telephone")]
            public string BusTelephone { get; set; }

            [Required(ErrorMessage = "Email Address is required")]
            [Display(Name = "Business Email Address")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
               ErrorMessage = "Email is is not valid.")]
            [DataType(DataType.EmailAddress)]
            public string BusinessEmail { get; set; }
        }
    }
}
