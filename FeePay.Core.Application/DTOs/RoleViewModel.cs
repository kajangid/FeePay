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
    public class RoleViewModel : BaseViewModel
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


        public IEnumerable<MvcControllerInfo> SelectedControllers { get; set; }

        public string Access { get; set; }
        public List<CheckBoxItem> UserList { get; set; }

    }
}
