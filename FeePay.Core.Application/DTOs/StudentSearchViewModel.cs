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
    public class StudentSearchViewModel
    {
        [Required(ErrorMessage = "Field is required.")]
        [DisplayName("Class")]
        public int ClassId { get; set; }
        [DisplayName("Section")]
        public int? SectionId { get; set; }
        [DisplayName("Category")]
        public string Category { get; set; }
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        [DisplayName("Search")]
        public string Search { get; set; }


        public StudentSearchViewModel()
        {
            Students = new List<StudentAdmissionViewModel>();
            Classes = new List<DropDownItem>();
            Categories = new List<DropDownItem>();
            Sessions = new List<DropDownItem>();
            Sections = new List<DropDownItem>();
        }
        public List<StudentAdmissionViewModel> Students { get; set; }
        public List<DropDownItem> Classes { get; set; }
        public List<DropDownItem> Sections { get; set; }
        public List<DropDownItem> Sessions { get; set; }
        public List<DropDownItem> Categories { get; set; }

    }
}
