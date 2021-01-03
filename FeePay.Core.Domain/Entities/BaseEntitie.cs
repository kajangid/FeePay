using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities
{
    public class BaseEntitie
    {
        public bool IsActive { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public DateTime? ModifyDate { get; set; }
        public int ModifyBy { get; set; } = 0;
        public int AddedBy { get; set; } = 0;
        public DateTime? AddedDate { get; set; }
    }
}
