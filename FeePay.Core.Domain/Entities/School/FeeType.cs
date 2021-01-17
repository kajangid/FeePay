using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities.School
{
    public class FeeType : BaseEntitie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }




        public SchoolAdminUser AddedByUser { get; set; }
        public SchoolAdminUser ModifyByUser { get; set; }
    }
}
