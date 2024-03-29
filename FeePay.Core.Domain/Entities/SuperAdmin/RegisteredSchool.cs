﻿using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities.SuperAdmin
{
    public class RegisteredSchool : BaseEntitie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string PrincipalName { get; set; }
        public string ContactNumber { get; set; }
        public bool IsApproved { get; set; }
        public string SchoolImage { get; set; }
        public string UniqueId { get; set; }
        public string Address { get; set; }

        public SuperAdminUser AddedByUser { get; set; }
        public SuperAdminUser ModifyByUser { get; set; }
    }
}
