using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Core.Domain.Entities.Student
{
    public class StudentFees
    {
        public int Id { get; set; }
        public int StudentAdmissionId { get; set; }
        public int AcademicSessionId { get; set; }
        public int FeeMasterId { get; set; }
        public string Status { get; set; }
        public string Mode { get; set; }
        public string PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaid { get; set; }
        public string Receipt { get; set; }




        public int FeeGroupId { get; set; }
        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }
        public string FeeTypeCode { get; set; }
        public string FeeGroupName { get; set; }

        public decimal FeeAmount { get; set; }
        public DateTime? DueDate { get; set; }

        public FeesTranscation FeesTranscation { get; set; }
        public StudentAdmission StudentAdmission { get; set; }
    }
}
