using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class ClassFeeSummaryViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string ClassSectionName { get; set; }
        public decimal TotalFees { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
