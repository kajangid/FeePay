using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using FeePay.Core.Application.Behaviors;
using static FeePay.Core.Application.Enums.FileTypeEnum;

namespace FeePay.Core.Application.DTOs
{
    public class StudentAdmissionViewModel : BaseViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(49)]
        [DisplayName("Form Number")]
        public string FormNo { get; set; }
        [Required(ErrorMessage = "Admission Date Required.")]
        [DisplayName("Admission Date")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AdmissionDate { get; set; }
        [Required]
        [DisplayName("Class")]
        public int ClassId { get; set; }
        [Required]
        [DisplayName("Section")]
        public int SectionId { get; set; }
        [Required]
        [DisplayName("Sr/Reg Number")]
        [StringLength(79)]
        public string Sr_RegNo { get; set; }
        [DisplayName("Enroll No")]
        [StringLength(49)]
        public string EnrollNo { get; set; }
        [DisplayName("MACHINE ID")]
        [StringLength(49)]
        public string MACHINEID { get; set; }
        [StringLength(49)]
        [DisplayName("Student Type")]
        public string StudentType { get; set; }
        [StringLength(49)]
        [DisplayName("Student Medium")]
        public string Medium { get; set; }

        [StringLength(349)]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        public int StudentLoginId { get; set; }

        #region Contact & Personal Info
        [Required]
        [StringLength(49)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(49)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(49)]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(49)]
        [DisplayName("Category")]
        public string Category { get; set; }
        [Required]
        [RegularExpression(@"^((\+91?)|\+)?[7-9][0-9]{9}$", ErrorMessage = "Please enter a valid mobile number.")]
        [DisplayName("Student Mobile Number")]
        public string MobileNo { get; set; }
        [StringLength(59)]
        [DisplayName("Student Email")]
        public string StudentEmail { get; set; }

        public string Image { get; set; }
        #endregion

        #region Previous Academic Info 
        [DisplayName("Previous Class")]
        [StringLength(49)]
        public string PreviousClass { get; set; }
        [DisplayName("Previous Institute Name")]
        [StringLength(249)]
        public string PreviousInstituteName { get; set; }
        [DisplayName("Year Of Passing")]
        [StringLength(49)]
        public string YearOfPassing { get; set; }
        [DisplayName("Previous Roll Number")]
        [StringLength(49)]
        public string PreviousRollNo { get; set; }
        [DisplayName("Previous Percent")]
        [StringLength(49)]
        public string PreviousPercent { get; set; }
        #endregion

        #region Guardian
        [Required]
        [StringLength(49)]
        [DisplayName("Father's Name")]
        public string FatherName { get; set; }
        [StringLength(49)]
        [DisplayName("Mother's Name")]
        public string MotherName { get; set; }
        [DisplayName("Guardian Mobile Number")]
        [RegularExpression(@"^((\+91?)|\+)?[7-9][0-9]{9}$", ErrorMessage = "Please enter a valid mobile number.")]
        public string GuardianMobileNo { get; set; }
        [StringLength(59)]
        [DisplayName("Guardian Email")]
        public string GuardianEmail { get; set; }
        [RegularExpression(@"^((\+91?)|\+)?[7-9][0-9]{9}$", ErrorMessage = "Please enter a valid mobile number.")]
        [DisplayName("Guardian Alternate Mobile Number")]
        public string AlternateMobileNo { get; set; }
        #endregion

        #region Address
        [StringLength(499)]
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("City")]
        public int CityId { get; set; }
        [DisplayName("Village")]
        public string Village { get; set; }
        [DisplayName("State")]
        public int StateId { get; set; }
        [StringLength(49)]
        [DisplayName("Religion")]
        public string Religion { get; set; }
        #endregion


        public StudentLogin LoginInfo { get; set; }

        public Classes StudentClass { get; set; }
        public Section StudentSection { get; set; }
        public string Fullname { get; set; }


        public List<DropDownItem> AvaliableClasses { get; set; }
        public List<DropDownItem> StatesDDL { get; set; }

        [DisplayName("Student Image")]
        [DataType(DataType.Upload)]
        [MaxFileSize(1 * 1024)]
        [FileTypeExtensions(new FileType[] { FileType.Jpeg, FileType.Png, FileType.Jpg })]
        public IFormFile FormImage { get; set; }

    }
}
