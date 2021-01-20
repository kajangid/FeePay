using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Service.School
{
    public interface IAcademicServices
    {

        #region Class
        Task<ClassViewModel> BindClassViewModelAsync(ClassViewModel model = null);
        Task<Response<List<ClassViewModel>>> GetListOfClassesAsync();
        Task<Response<ClassViewModel>> FindClassByIdAsync(int Id);
        Task<Response<bool>> AddOrEditClassAsync(ClassViewModel model);

        #endregion

        #region Section
        Task<Response<List<SectionViewModel>>> GetListOfSectionsAsync();
        Task<Response<SectionViewModel>> FindSectionByIdAsync(int Id);
        Task<Response<bool>> AddOrEditSectionAsync(SectionViewModel model);

        #endregion

        #region Session
        Task<Response<List<SessionViewModel>>> GetListOfSessionsAsync();
        Task<Response<SessionViewModel>> FindSessionByIdAsync(int Id);
        Task<Response<bool>> AddOrEditSessionAsync(SessionViewModel model);

        #endregion
    }
}
