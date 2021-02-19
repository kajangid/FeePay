using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FeePay.Core.Application.Behaviors;
using static FeePay.Core.Application.Enums.FileTypeEnum;
using FeePay.Core.Domain.Entities;

namespace FeePay.Core.Application.DTOs
{
    public class PaymentGatewayDocumentViewModel : BaseViewModel
    {
        public int Id { get; set; }

        #region Step 1 Buisness Details
        [Required(ErrorMessage = "Display Name filed required.")]
        [StringLength(maximumLength: 25, MinimumLength = 5, ErrorMessage = "Display Name must be 5 to 25 character long.")]
        [DisplayName("Display Name")]
        public string BusinessDisplayName { get; set; }

        [Required(ErrorMessage = "PAN Card Number filed required.")]
        [StringLength(maximumLength: 25, MinimumLength = 5, ErrorMessage = "PAN Card Number must be 5 to 25 character long.")]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "PAN Card Number is invalid.")]
        [DisplayName("Display Name")]
        public string BusinessPANCardNumber { get; set; }


        //[Required(ErrorMessage = "PAN Card filed required.")]
        [DisplayName("PAN Card")]
        [DataType(DataType.Upload)]
        [MaxFileSize(1 * 1024)]
        [FileTypeExtensions(new FileType[] { FileType.Jpeg, FileType.Png, FileType.Jpg })]
        public IFormFile BusinessPANCard_File { get; set; }
        public string BusinessPANCard { get; set; }
        public bool BusinessPANCardSaveAsDocument { get; set; }
        public Upload BusinessPANCardUploadData { get; set; }

        [Required(ErrorMessage = "Registered Address filed required.")]
        [StringLength(maximumLength: 125, MinimumLength = 5, ErrorMessage = "Registered Address must be 5 to 125 character long.")]
        [DisplayName("Registered Address")]
        public string RegisteredAddress { get; set; }

        [Required(ErrorMessage = "Pin Code filed required.")]
        [StringLength(maximumLength: 10, MinimumLength = 5, ErrorMessage = "Pin Code must be 5 to 10 character long.")]
        [DisplayName("Pin Code")]
        public string AddressPinCode { get; set; }

        [Required(ErrorMessage = "City filed required.")]
        [DisplayName("City")]
        public int AddressCityId { get; set; }

        [Required(ErrorMessage = "State filed required.")]
        [DisplayName("State")]
        public int AddressStateId { get; set; }
        #endregion

        #region Step 2 Bank Account Details

        [Required(ErrorMessage = "Account Number filed required.")]
        [RegularExpression(@"^\d{9,18}$", ErrorMessage = "Account Number is invalid.")]
        [StringLength(maximumLength: 18, MinimumLength = 9, ErrorMessage = "Account Number must be 9 to 18 character long.")]
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Confirm Account Number filed required.")]
        [Compare("AccountNumber", ErrorMessage = "Account Number and Confirmation Account Number do not match.")]
        [DisplayName("Confirm Account Number")]
        public string ConfirmAccountNumber { get; set; }

        [Required(ErrorMessage = "Bank IFSC Code filed required.")]
        [RegularExpression(@"^[A-Z]{4}0[A-Z0-9]{6}$", ErrorMessage = "IFSC Code is invalid.")]
        [StringLength(maximumLength: 25, MinimumLength = 3, ErrorMessage = "Bank IFSC Code must be 3 to 25 character long.")]
        [DisplayName("Bank IFSC Code")]
        public string IFSC { get; set; }

        //[Required(ErrorMessage = "Xerox Copy Of Bank Passbook filed required.")]
        [DisplayName("Xerox Copy Of Bank Passbook")]
        [DataType(DataType.Upload)]
        [MaxFileSize(1 * 1024)]
        [FileTypeExtensions(new FileType[] { FileType.Jpeg, FileType.Png, FileType.Jpg })]
        public IFormFile XeroxCopyOfBankPassbook_File { get; set; }
        public string XeroxCopyOfBankPassbook { get; set; }
        public bool XeroxCopyOfBankPassbookSaveAsDocument { get; set; }
        public Upload BankPassbookUploadData { get; set; }


        #endregion

        #region Step 3 Primary Contact Details
        [Required(ErrorMessage = "Contact Name Field Require.")]
        [StringLength(maximumLength: 49, MinimumLength = 3, ErrorMessage = "Contact Name must be 3 to 49 character long.")]
        [DisplayName("Name of Primary Contact")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Contact Email Field Require.")]
        [EmailAddress]
        [DisplayName("Contact Email")]
        [RegularExpression(@"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$", ErrorMessage = "Please enter a valid email.")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Contact Mobile Number Field Require.")]
        [RegularExpression(@"^((\+91?)|\+)?[1-9][0-9]{9}$", ErrorMessage = "Please enter a valid mobile number.")]
        [DisplayName("Contact Mobile Number")]
        public string ContactPhoneNumber { get; set; }

        //[Required(ErrorMessage = "Identity Proof filed required.")]
        [DisplayName("Identity Proof")]
        [DataType(DataType.Upload)]
        [MaxFileSize(1 * 1024)]
        [FileTypeExtensions(new FileType[] { FileType.Jpeg, FileType.Png, FileType.Jpg })]
        public IFormFile IdentityProof_File { get; set; }
        public string IdentityProof { get; set; }
        public bool IdentityProofSaveAsDocument { get; set; }
        public Upload IdentityProofUploadData { get; set; }

        #endregion

        public bool IsApproved { get; set; }
        public int RegisteredSchoolId { get; set; }
        public RegisterSchoolViewModel RegisterSchool { get; set; }
    }
}
