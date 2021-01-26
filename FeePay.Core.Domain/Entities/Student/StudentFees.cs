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
        public int StudentId { get; set; }
        public int FeeGroupId { get; set; }
        public int FeeMasterId { get; set; }
        public string FeePaymentId { get; set; }
        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }
        public string FeeTypeCode { get; set; }
        public string FeeGroupName { get; set; }
        public string FeeStatus { get; set; }

        public decimal FeeAmount { get; set; }
        public DateTime? FeePaymentDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Mode { get; set; }

    }
}
