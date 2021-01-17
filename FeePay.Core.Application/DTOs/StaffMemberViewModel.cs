using FeePay.Core.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class StaffMemberViewModel : BaseViewModel
    {
        public StaffMemberViewModel()
        {
            RoleList = new List<CheckBoxItem>();
        }
        [DisplayName("User Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required for adding a staff member.")]
        [StringLength(49)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [StringLength(49)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mobile Number is required for adding a staff member.")]
        [DisplayName("Mobile Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("User Name")]
        [StringLength(49)]
        public string UserName { get; set; }

        [DisplayName("Email Address")]
        public string Email { get; set; }

        [DisplayName("A Initial Password")]
        public string Password { get; set; }

        [DisplayName("Last Login Time")]
        public DateTime? LastLoginDate { get; set; }



        public string RoleListString { get; set; }
        public List<CheckBoxItem> RoleList { get; set; }

        [DisplayName("User Image")]
        public IFormFile formFile { get; set; }
    }
}
