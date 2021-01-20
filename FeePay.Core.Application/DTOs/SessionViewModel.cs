using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class SessionViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Year")]
        [StringLength(19)]
        public string Year { get; set; }

        [DisplayName("Start Year")]
        [StringLength(19)]
        public string StartYear { get; set; }

        [DisplayName("End Year")]
        [StringLength(19)]
        public string EndYear { get; set; }

        [DisplayName("Description")]
        [StringLength(349)]
        public string Description { get; set; }
    }
}
