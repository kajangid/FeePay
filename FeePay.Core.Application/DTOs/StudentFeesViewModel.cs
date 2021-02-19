using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class StudentFeesViewModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int AcademicSessionId { get; set; }
        public int StudentAdmissionId { get; set; }
        public int FeeMasterId { get; set; }
        public int FeeGroupId { get; set; }
        public string Status { get; set; }
        public string Mode { get; set; }
        public string PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }


        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }
        public string FeeTypeCode { get; set; }
        public string FeeGroupName { get; set; }

        public decimal FeeAmount { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsPaid { get; set; }

        public bool IsSelected { get; set; }

        public StudentAdmissionViewModel StudentAdmission { get; set; }
    }
}
