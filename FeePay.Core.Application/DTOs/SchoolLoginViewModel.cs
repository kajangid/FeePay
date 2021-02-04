using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class SchoolLoginViewModel : CommonLoginViewModel
    {
        [StringLength(30)]
        [DisplayName("Username")]
        [Required(ErrorMessage = "Please fill Username field.")]
        //[RegularExpression(@"^[a-zA-Z][a-zA-Z0-9\._\-]{0,18}?[a-zA-Z0-9]{0,2}$", ErrorMessage = "Please enter a valid Username.")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("School Code")]
        [StringLength(30)]
        public string SchoolUniqueId { get; set; }
    }
}
