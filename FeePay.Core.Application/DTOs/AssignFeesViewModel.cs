using FeePay.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class AssignFeesViewModel : StudentSearchViewModel
    {
        public AssignFeesViewModel()
        {
            StudentAdmissionList = new List<StudentAdmissionViewModel>();
            CbStudents = new List<CheckBoxItem>();
        }
        public FeeGroupViewModel FeeGroup { get; set; }
        public List<StudentAdmissionViewModel> StudentAdmissionList { get; set; }

        public List<CheckBoxItem> CbStudents { get; set; }
    }
}
