using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class FeeCollectionSearchModel
    {
        [Required(ErrorMessage = "Field Required.")]
        [DisplayName("Admission Date")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessage = "Field Required.")]
        [DisplayName("Admission Date")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }
    }
}
