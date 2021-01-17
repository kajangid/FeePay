using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class BaseViewModel
    {

        public bool IsActive { get; set; }

        public string ModifyBy { get; set; }
        public int ModifyById { get; set; }
        public DateTime? ModifyDate { get; set; }

        public string AddedBy { get; set; }
        public int AddedById { get; set; }
        public DateTime? AddedDate { get; set; }
    }
}
