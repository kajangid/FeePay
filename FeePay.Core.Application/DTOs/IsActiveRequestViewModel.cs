using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class IsActiveRequestViewModel
    {
        public int DataId { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public string Name { get; set; }
    }
}
