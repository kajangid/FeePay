using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            Roles = new List<RoleViewModel>();
        }

        [DisplayName("User Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please fill Username field.")]
        [DisplayName("Username")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\._\-]{0,18}?[a-zA-Z0-9]{0,2}$", ErrorMessage = "Please enter a valid Username.")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Phone]
        [DisplayName("Mobile Number")]
        [RegularExpression(@"((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}[0-9][0-9](\s){0,1}(\-){0,1}(\s){0,1}[1-9]{1}(\s){0,1}(\-){0,1}(\s){0,1}([0-9]{1}(\s){0,1}(\-){0,1}(\s){0,1}){1,6}[0-9]{1}", ErrorMessage = "Please enter a valid mobile number.")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [DisplayName("Email Address")]
        [RegularExpression(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$", ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }

        [DisplayName("Last Login Time")]
        public DateTime? LastLoginDate { get; set; }





        [DisplayName("User Roles")]
        public List<RoleViewModel> Roles { get; set; }

        public ResetPasswordViewModel ResetPassword { get; set; }

        public UserPasswordViewModel UserNamePass { get; set; }
    }
}
