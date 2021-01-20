using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class SectionViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        [StringLength(49, ErrorMessage = "Name character length exceed the required length.")]
        public string Name { get; set; }

        [DisplayName("Name")]
        [StringLength(349, ErrorMessage = "Description character length exceed the required length.")]
        public string Description { get; set; }
    }
}
