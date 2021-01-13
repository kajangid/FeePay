using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities.Identity
{
    public class SchoolAdminUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string LastLoginIP { get; set; }
        public DateTime? LastLoginDate { get; set; }


        // Temp Fields to store runtime data
        public string SchoolUniqueId { get; set; }
        public string SchoolName { get; set; }
        public List<string> Roles { get; set; }



        public SchoolAdminUser AddedByUser { get; set; }
        public SchoolAdminUser ModifyByUser { get; set; }



    }
}
