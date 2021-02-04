using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class FeeDepositSummeryViewModel
    {
        public FeeDepositSummeryViewModel()
        {
            Fees = new List<StudentFeesViewModel>();
        }
        public StudentAdmissionViewModel Student { get; set; }
        public List<StudentFeesViewModel> Fees { get; set; }

    }
}
