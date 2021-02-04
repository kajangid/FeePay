using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class SelectedFeeDepositViewModel
    {
        public List<SelectedFeeDeposit> FeeDeposit { get; set; }
    }
    public class SelectedFeeDeposit
    {
        public int StudentFeeId { get; set; }
        public string FeeTypeCode { get; set; }
        public bool IsSelected { get; set; }
    }
}
