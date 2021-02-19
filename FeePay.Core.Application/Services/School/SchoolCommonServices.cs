using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Application.Interface.Service.School;

namespace FeePay.Core.Application.Services.School
{
    public class SchoolCommonServices : ISchoolCommonServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppContextAccessor _appContextAccessor;
        public SchoolCommonServices(IUnitOfWork unitOfWork,
            IAppContextAccessor appContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _appContextAccessor = appContextAccessor;
        }

        /// <summary>
        /// Section in Class 
        /// </summary>
        /// <param name="classId"> Class Id </param>
        /// <returns> List of DropDownItem of Sections </returns>
        public async Task<Response<List<DropDownItem>>> DDL_ClassSectionsAsync(int classId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var _class = await _unitOfWork.ClassSection.FindSectionsInClassByClassIdAsync(classId, SchoolId);
            List<DropDownItem> ddl = _class.Sections?.Select(s => new DropDownItem { Text = s.NormalizedName, Value = s.Id.ToString() }).ToList();
            return new Response<List<DropDownItem>>(ddl ?? new List<DropDownItem>());
        }

        /// <summary>
        /// Cities in State
        /// </summary>
        /// <param name="stateId"> State Id </param>
        /// <returns> List of DropDownItem of Cities </returns>
        public async Task<Response<List<DropDownItem>>> DDL_StateCitiesAsync(int stateId)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var cities = await _unitOfWork.CityState.FindActiveCitiesByStateIdAsync(stateId, SchoolId);
            List<DropDownItem> ddl = cities?.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            return new Response<List<DropDownItem>>(ddl ?? new List<DropDownItem>());
        }

        /// <summary>
        /// Gets States
        /// </summary>
        /// <returns> List of DropDownItem of Cities </returns>
        public async Task<Response<List<DropDownItem>>> DDL_StatesAsync()
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var states = await _unitOfWork.CityState.GetAllActiveStatesAsync(schoolId);
            List<DropDownItem> ddl = states?.Select(s => new DropDownItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            return new Response<List<DropDownItem>>(ddl ?? new List<DropDownItem>());
        }

        /// <summary>
        /// Gets Classes 
        /// </summary>
        /// <returns> List of DropDownItem of Classes </returns>
        public async Task<Response<List<DropDownItem>>> DDL_ClassesAsync()
        {
            var schoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var _classes = await _unitOfWork.ClassRepo.GetAllAsync(dbId: schoolId, isActive: true);
            List<DropDownItem> ddl = _classes?.Select(s => new DropDownItem { Text = s.NormalizedName, Value = s.Id.ToString() }).ToList();
            return new Response<List<DropDownItem>>(ddl ?? new List<DropDownItem>());
        }

        /// <summary>
        /// Gets State
        /// </summary>
        /// <param name="id"></param>
        /// <returns> DropDownItem of State </returns>
        public async Task<Response<DropDownItem>> DDL_StateByIdAsync(int id)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var cities = await _unitOfWork.CityState.FindActiveStateByIdAsync(id, SchoolId);
            if (cities == null) return new Response<DropDownItem>("No data found.");
            var ddl = new DropDownItem { Text = cities.Name, Value = cities.Id.ToString() };
            return new Response<DropDownItem>(ddl ?? new DropDownItem());
        }

        /// <summary>
        /// Gets City
        /// </summary>
        /// <param name="id"></param>
        /// <returns> DropDownItem of City </returns>
        public async Task<Response<DropDownItem>> DDL_CityByIdAsync(int id)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var cities = await _unitOfWork.CityState.FindActiveCityByIdAsync(id, SchoolId);
            if (cities == null) return new Response<DropDownItem>("No data found.");
            var ddl = new DropDownItem { Text = cities.Name, Value = cities.Id.ToString() };
            return new Response<DropDownItem>(ddl ?? new DropDownItem());
        }
    }
}
