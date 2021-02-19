using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities.SuperAdmin
{
	public class PaymentGatewayDocument : BaseEntitie
	{
		public int Id { get; set; }
		public string BusinessDisplayName { get; set; }
		public string BusinessPANCardNumber { get; set; }
		public string BusinessPANCardCopy { get; set; }
		public string RegisteredAddress { get; set; }
		public string AddressPinCode { get; set; }
		public int AddressCityId { get; set; }
		public int AddressStateId { get; set; }
		public string AccountNumber { get; set; }
		public string IFSC { get; set; }
		public string BankPassbookCopy { get; set; }
		public string ContactName { get; set; }
		public string ContactEmail { get; set; }
		public string ContactPhoneNumber { get; set; }
		public string IdentityProof { get; set; }
		public int RegisteredSchoolId { get; set; }
		public bool IsApproved { get; set; }



		public SchoolAdminUser AddedByUser { get; set; }
		public SchoolAdminUser ModifyByUser { get; set; }
		public RegisteredSchool RegisteredSchool { get; set; }

	}
}
