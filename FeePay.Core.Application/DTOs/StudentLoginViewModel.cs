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
    public class StudentLoginViewModel : CommonLoginViewModel
    {

        [Required]
        [DisplayName("School Code")]
        public string SchoolUniqueId { get; set; }
    }
}
