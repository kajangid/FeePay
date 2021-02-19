using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Common;

namespace FeePay.Core.Application.DTOs
{
    public class FeePendingViewModel
    {
        public FeePendingViewModel()
        {
            StudentFeesList = new List<StudentFeesViewModel>();
            CbStudents = new List<CheckBoxItem>();
        }
        public List<StudentFeesViewModel> StudentFeesList { get; set; }
        public List<CheckBoxItem> CbStudents { get; set; }
        public StudentSearchViewModel SearchModel { get; set; }
    }
}
