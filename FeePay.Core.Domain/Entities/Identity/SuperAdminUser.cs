using System;
using System.Collections.Generic;
using System.Text;

namespace FeePay.Core.Domain.Entities.Identity
{
    public class SuperAdminUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public string City { get; set; }
        public string LastLoginIP { get; set; }
        public DateTime? LastLoginDate { get; set; }

    }
}
