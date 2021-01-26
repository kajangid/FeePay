using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Domain.Entities.Common;

namespace FeePay.Core.Application.Interface.Service
{
    public interface ICommanDataServices
    {
        List<DropDownItem> GetStudentCategoryDDLItems();
        List<DropDownItem> GetAdmissionTypeDDLItems();
        List<DropDownItem> GetReligionDDLItems();
    }
}
