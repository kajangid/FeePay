using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Domain.Entities.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities.Student
{
    public class StudentAdmission : BaseEntitie
    {
        public StudentAdmission()
        {
            FeesGroupList = new List<FeeGroup>();
        }
        public int Id { get; set; }
        public string Sr_RegNo { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public int AcademicSessionId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string FormNo { get; set; }
        public string EnrollNo { get; set; }
        public string MACHINEID { get; set; }
        public string StudentType { get; set; }
        public string Medium { get; set; }
        public string Remarks { get; set; }
        public int StudentLoginId { get; set; }

        #region Contact & Personal Info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Category { get; set; }
        public string MobileNo { get; set; }
        public string StudentEmail { get; set; }
        public string Image { get; set; }
        #endregion

        #region Previous Academic Info 
        public string PreviousClass { get; set; }
        public string PreviousInstituteName { get; set; }
        public string YearOfPassing { get; set; }
        public string PreviousRollNo { get; set; }
        public string PreviousPercent { get; set; }
        #endregion

        #region Guardian
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string GuardianMobileNo { get; set; }
        public string GuardianEmail { get; set; }
        public string AlternateMobileNo { get; set; }
        #endregion

        #region Address
        public string Address { get; set; }
        public int CityId { get; set; }
        public string Village { get; set; }
        public int StateId { get; set; }
        public string Religion { get; set; }
        #endregion


        public StudentLogin LoginInfo { get; set; }
        public List<FeeGroup> FeesGroupList { get; set; }

        public Classes StudentClass { get; set; }
        public Section StudentSection { get; set; }

        public SchoolAdminUser AddedByUser { get; set; }
        public SchoolAdminUser ModifyByUser { get; set; }
    }
}
