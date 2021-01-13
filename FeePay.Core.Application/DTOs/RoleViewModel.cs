using FeePay.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            UserList = new List<CheckBoxItem>();
        }
        [DisplayName("Role Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Role name is required for creating role.")]
        [StringLength(49)]
        [DisplayName("Role Name")]
        public string Name { get; set; }
        [DisplayName("Role Active")]
        public bool IsActive { get; set; }
        public int ModifyById { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int AddedById { get; set; }
        public string AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }

        public List<CheckBoxItem> UserList { get; set; }

    }
}
