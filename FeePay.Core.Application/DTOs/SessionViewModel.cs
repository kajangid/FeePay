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

        [DisplayName("Start Date")]
        public DateTime? StartYear { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndYear { get; set; }

        [DisplayName("Description")]
        [StringLength(349)]
        public string Description { get; set; }
    }
}
