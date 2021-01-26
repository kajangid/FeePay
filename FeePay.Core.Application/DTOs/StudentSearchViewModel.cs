using FeePay.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class StudentSearchViewModel
    {
        [Required]
        public int ClassId { get; set; }
        public int? SectionId { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }

        [Required]
        public string Search { get; set; }



        public List<DropDownItem> Classes { get; set; }
        public List<DropDownItem> Categories { get; set; }
    }
}
