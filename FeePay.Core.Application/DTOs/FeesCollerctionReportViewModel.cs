using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class FeesCollerctionReportViewModel
    {
        public FeesCollerctionReportViewModel()
        {
            StudentFees = new List<StudentFeesViewModel>();
        }
        public int StudentAdmissionId { get; set; }
        public string FeeTransactionId { get; set; }
        public int ClassID { get; set; }
        public int SectionId { get; set; }

        public decimal Deposit { get; set; }
        public decimal Discount { get; set; }

        public string StudentName { get; set; }
        public string FormNo { get; set; }
        public string Sr_RegNo { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string MobileNo { get; set; }
        public DateTime? AdmissionDate { get; set; }

        public string ClassName { get; set; }
        public string SectionName { get; set; }

        public string Receipt { get; set; }
        public string Mode { get; set; }

        public List<StudentFeesViewModel> StudentFees { get; set; }
        public FeeCollectionSearchModel SearchModel { get; set; }
    }
}
