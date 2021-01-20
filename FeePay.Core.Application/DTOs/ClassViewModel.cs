using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Core.Application.DTOs
{
    public class ClassViewModel : BaseViewModel
    {
        public ClassViewModel()
        {
            Sections = new List<Section>();
            CBSections = new List<CheckBoxItem>();
        }
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        [StringLength(49, ErrorMessage = "Name character length exceed the required length.")]
        public string Name { get; set; }

        [DisplayName("Name")]
        [StringLength(349, ErrorMessage = "Description character length exceed the required length.")]
        public string Description { get; set; }


        public List<Section> Sections { get; set; }
        public List<CheckBoxItem> CBSections { get; set; }
    }
}

