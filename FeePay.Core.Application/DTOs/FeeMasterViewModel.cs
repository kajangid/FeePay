using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Core.Application.DTOs
{
    public class FeeMasterViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Fee Type")]
        public int FeeTypeId { get; set; }

        [Required]
        [DisplayName("Fee Group")]
        public int FeeGroupId { get; set; }


        [DisplayName("Due Date")]
        public DateTime? DueDate { get; set; }

        [Required]
        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [StringLength(349)]
        public string Description { get; set; }


        public FeeType FeeType { get; set; }
        public FeeGroup FeeGroup { get; set; }

        public List<DropDownItem> FeeTypeList { get; set; }
        public List<DropDownItem> FeeGroupList { get; set; }
    }
}
