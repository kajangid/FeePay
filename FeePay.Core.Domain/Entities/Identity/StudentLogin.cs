using FeePay.Core.Domain.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities.Identity
{
    public class StudentLogin : IdentityUser
    {
        public string Password { get; set; }
        public bool IsLogin { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastLoginIP { get; set; }
        public string LastLoginDevice { get; set; }
        public string LastLoginLocation { get; set; }

        public StudentAdmission StudentAdmission { get; set; }

        // Temp Fields to store runtime data
        public string SchoolUniqueId { get; set; }
        public string SchoolName { get; set; }

    }
}
