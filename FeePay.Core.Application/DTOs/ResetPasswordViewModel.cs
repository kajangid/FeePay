using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class ResetPasswordViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Current Password is required")]
        [DisplayName("Current Password")]
        [DataType(DataType.Password)]
        [StringLength(16)]
        public string CurrentPassword { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DisplayName("New Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,16}$",
            ErrorMessage = "Password must have <br/>" +
            "At least one upper case letter. <br/>" +
            "At least one lower case letter. <br/>" +
            "At least one digit. <br/>" +
            "At least one special character(#?!@$%^&*-). <br/>" +
            "Minimum eight and maximum sixteen in length")]
        public string NewPassword { get; set; }


        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassword { get; set; }

    }
}
