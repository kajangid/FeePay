using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities.School
{
    public class FeeMaster : BaseEntitie
    {
        public int Id { get; set; }
        public int FeeTypeId { get; set; }
        public int FeeGroupId { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int AcademicSessionId { get; set; }


        public FeeType FeeType { get; set; }
        public FeeGroup FeeGroup { get; set; }


        public SchoolAdminUser AddedByUser { get; set; }
        public SchoolAdminUser ModifyByUser { get; set; }

    }
}
