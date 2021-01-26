using FeePay.Core.Domain.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class StudentLedgerViewModel
    {
        public StudentAdmissionViewModel StudentAdmissionViewModel { get; set; }
        public List<StudentFees> FeeList { get; set; }

        // TODO Add All Fee Transaction And Others
    }
}
