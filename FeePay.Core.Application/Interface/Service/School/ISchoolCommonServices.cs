using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;

namespace FeePay.Core.Application.Interface.Service.School
{
    public interface ISchoolCommonServices
    {

        /// <summary>
        /// Section in Class 
        /// </summary>
        /// <param name="classId"> Class Id </param>
        /// <returns> List of DropDownItem of Sections </returns>
        Task<Response<List<DropDownItem>>> DDL_ClassSectionsAsync(int classId);

        /// <summary>
        /// Cities in State
        /// </summary>
        /// <param name="stateId"> State Id </param>
        /// <returns> List of DropDownItem of Cities </returns>
        Task<Response<List<DropDownItem>>> DDL_StateCitiesAsync(int stateId);

        /// <summary>
        /// Gets States
        /// </summary>
        /// <returns> List of DropDownItem of Cities </returns>
        Task<Response<List<DropDownItem>>> DDL_StatesAsync();

        /// <summary>
        /// Gets Classes 
        /// </summary>
        /// <returns> List of DropDownItem of Classes </returns>
        Task<Response<List<DropDownItem>>> DDL_ClassesAsync();

        /// <summary>
        /// Gets State
        /// </summary>
        /// <param name="id"></param>
        /// <returns> DropDownItem of State </returns>
        Task<Response<DropDownItem>> DDL_StateByIdAsync(int id);

        /// <summary>
        /// Gets City
        /// </summary>
        /// <param name="id"></param>
        /// <returns> DropDownItem of City </returns>
        Task<Response<DropDownItem>> DDL_CityByIdAsync(int id);
    }
}
