using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class FeesTransactionReportViewModel
    {
        public int StudentAdmissionId { get; set; }
        public string FeeTransactionId { get; set; }

        public decimal Deposit { get; set; }
        public decimal Discount { get; set; }

        public string StudentName { get; set; }
        public string Sr_RegNo { get; set; }
        public string FatherName { get; set; }
        public string MobileNo { get; set; }

        public string ClassName { get; set; }
        public string SectionName { get; set; }
    }
}
