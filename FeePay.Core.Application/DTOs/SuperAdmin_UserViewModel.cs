using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Identity;

namespace FeePay.Core.Application.DTOs
{
    public class SuperAdmin_UserViewModel : BaseViewModel
    {
        [DisplayName("User Id")]
        public int Id { get; set; }

        [StringLength(49)]
        [DisplayName("User Name")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\._\-]{0,18}?[a-zA-Z0-9]{0,2}$", ErrorMessage = "Please enter a valid Username.")]
        public string UserName { get; set; }

        [EmailAddress]
        [DisplayName("Email Address")]
        [RegularExpression(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$", ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }

        [Phone]
        [DisplayName("Mobile Number")]
        [RegularExpression(@"^((\+91?)|\+)?[7-9][0-9]{9}$", ErrorMessage = "Please enter a valid mobile number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(49)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(49)]
        public string LastName { get; set; }

        [DisplayName("Password")]
        [StringLength(49)]
        public string Password { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [DisplayName("Photo")]
        [StringLength(250)]
        public string Photo { get; set; }

        [DisplayName("City")]
        [StringLength(49)]
        public string City { get; set; }

        [DisplayName("Last Login IP")]
        [StringLength(49)]
        public string LastLoginIP { get; set; }

        [DisplayName("Last Login Date")]
        public DateTime? LastLoginDate { get; set; }


        public SuperAdminUser AddedByUser { get; set; }
        public SuperAdminUser ModifyByUser { get; set; }
    }
}
