using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;
using static FeePay.Core.Application.Enums.FileTypeEnum;

namespace FeePay.Core.Application.Interface.Service
{
    public interface ICommanDataServices
    {
        /// <summary>
        /// Get Student Category
        /// </summary>
        /// <returns> DropDownItem List Of Student Category </returns>
        List<DropDownItem> GetStudentCategoryDDLItems();

        /// <summary>
        /// Get Student Admission Type
        /// </summary>
        /// <returns> DropDownItem List Of Admission Types </returns>
        List<DropDownItem> GetAdmissionTypeDDLItems();

        /// <summary>
        /// Get Religions
        /// </summary>
        /// <returns> DropDownItem List Of Religions </returns>
        List<DropDownItem> GetReligionDDLItems();

        /// <summary>
        /// Use to get file mimetype enum
        /// </summary>
        /// <param name="fileNameWithExtension"></param>
        /// <returns> file type enum </returns>
        FileType GetFileTypeEnum(string fileNameWithExtension);
    }
}
