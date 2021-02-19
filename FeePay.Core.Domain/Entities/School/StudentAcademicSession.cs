using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Identity;

namespace FeePay.Core.Domain.Entities.School
{
    public class StudentAcademicSession : BaseEntitie
    {
        public int Id { get; set; }
        public int StudentAdmissionId { get; set; }
        public int SessionId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }

        // TODO: Set in db
        public string RollNumber { get; set; }

        public SchoolAdminUser AddedByUser { get; set; }
        public SchoolAdminUser ModifyByUser { get; set; }
    }
}
