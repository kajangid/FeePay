using FeePay.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.DTOs
{
    public class StudentLoginViewModel : CommonLoginViewModel
    {
        public List<DropDownItem> ActiveSchools { get; set; }
    }
}
