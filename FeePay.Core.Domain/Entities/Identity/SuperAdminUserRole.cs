using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities.Identity
{
    public class SuperAdminUserRole : BaseEntitie
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? BeginDate { get; set; }

    }
}
