using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.Student;

namespace FeePay.Core.Application.DTOs
{
    public class StudentPromotionViewModel
    {
        public StudentPromotionViewModel()
        {
            Classes = new List<DropDownItem>();
            Sessions = new List<DropDownItem>();
            Sections = new List<DropDownItem>();
            StudentAdmission = new List<StudentAdmission>();
        }

        [DisplayName("Class")]
        [Required(ErrorMessage = "Class Field is required.")]
        public int ClassId_Post { get; set; }

        [DisplayName("Section")]
        [Required(ErrorMessage = "Section Field is required.")]
        public int? SectionId_Post { get; set; }

        [DisplayName("Session")]
        [Required(ErrorMessage = "Session Field is required.")]
        public int SessionId_Promo { get; set; }

        [DisplayName("Class")]
        [Required(ErrorMessage = "Class Field is required.")]
        public int ClassId_Promo { get; set; }

        [DisplayName("Section")]
        [Required(ErrorMessage = "Section Field is required.")]
        public int SectionId_Promo { get; set; }



        public List<DropDownItem> Classes { get; set; }
        public List<DropDownItem> Sections { get; set; }
        public List<DropDownItem> Sessions { get; set; }
        public List<StudentAdmission> StudentAdmission { get; set; }
    }
}
