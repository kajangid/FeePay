using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class AllFeeSummaryViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public decimal TotalFees { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
