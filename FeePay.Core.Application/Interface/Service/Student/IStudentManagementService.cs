using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Service.Student
{
    public interface IStudentManagementService
    {
        #region Student Admission
        Task<StudentAdmissionViewModel> BindStudentAdmissionViewModelAsync(StudentAdmissionViewModel model = null);
        Task<Response<List<StudentAdmissionViewModel>>> GetListOfStudentsAsync();
        Task<Response<StudentAdmissionViewModel>> FindStudentByIdAsync(int Id);
        Task<Response<bool>> AddOrEditStudentAsync(StudentAdmissionViewModel model);

        #endregion
        Task<Response<List<DropDownItem>>> ClassSectionsAsync(int classId);
        Task<Response<List<DropDownItem>>> StateCitiesAsync(int stateId);
        Task<Response<List<StudentAdmissionViewModel>>> GetListOfActiveStudentsByClassIdAsync(int classId);
        Task<Response<List<StudentAdmissionViewModel>>> SearchStudentAsync(string searchParam);
        Task<Response<StudentLedgerViewModel>> StudentLedgerAsync(int id);
        Task<Response<UserPasswordViewModel>> GetStudentPassword(int studentId);
    }
}
