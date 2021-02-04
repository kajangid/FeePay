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
    public class RegisterSchoolViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [StringLength(49)]
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please fill the Name filed.")]
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public string UniqueId { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Principal Name")]
        public string PrincipalName { get; set; }

        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }

        [DisplayName("Is Approved")]
        public bool IsApproved { get; set; }

        [DisplayName("School Main Image")]
        public string SchoolImage { get; set; }


        public SuperAdminUser AddedByUser { get; set; }
        public SuperAdminUser ModifyByUser { get; set; }
    }
}
