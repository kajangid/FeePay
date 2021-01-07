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
        [Required]
        [DisplayName("School Code")]
        [StringLength(30)]
        public string SchoolUniqueId { get; set; }
    }
}
