using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Entities
{
	// TODO: Change this for something good option
	public class Comman_Sp_School
	{
		public int ClassId { get; set; }
		public int SectionId { get; set; }
		public int FeeGroupId { get; set; }
		public int FeeMasterId { get; set; }
		public int FeeTypeId { get; set; }
		public int StudentFeesId { get; set; }
		public int StudentAdmissionId { get; set; }



		public string PaymentId { get; set; }
		public string Name { get; set; }
		public string NormalizedName { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
		public string Code { get; set; }
		public string Status { get; set; }
		public string Mode { get; set; }
		public bool IsPaid { get; set; }
		public DateTime? PaymentDate { get; set; }
		public DateTime? DueDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }
		public DateTime? ModifyDate { get; set; }
		public int ModifyBy { get; set; }
		public DateTime? AddedDate { get; set; }
		public int AddedBy { get; set; }
		}
	}
