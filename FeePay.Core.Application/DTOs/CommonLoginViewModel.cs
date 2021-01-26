using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class CommonLoginViewModel
    {
        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        [RegularExpression(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$", ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }

        // TODO: Clear Validation Bug
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [StringLength(20)]
        //[RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,16}$",
        //    ErrorMessage = "Password must have <br/>" +
        //    "At least one upper case letter. <br/>" +
        //    "At least one lower case letter. <br/>" +
        //    "At least one digit. <br/>" +
        //    "At least one special character(#?!@$%^&*-). <br/>" +
        //    "Minimum eight and maximum sixteen in length")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string UserIp { get; set; }
    }
}
