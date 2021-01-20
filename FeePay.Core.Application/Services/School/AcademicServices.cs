using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Application.Interface.Repository;
using FeePay.Core.Application.Interface.Service;
using FeePay.Core.Application.Interface.Service.School;
using FeePay.Core.Application.Wrapper;
using FeePay.Core.Domain.Entities.Common;
using FeePay.Core.Domain.Entities.School;

namespace FeePay.Core.Application.Services.School
{
    public class AcademicServices : IAcademicServices
    {
        public AcademicServices(IUnitOfWork unitOfWork, IMapper mapper, IAppContextAccessor appContextAccessor,
            ILoginService loginService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appContextAccessor = appContextAccessor;
            _loginService = loginService;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppContextAccessor _appContextAccessor;
        private readonly ILoginService _loginService;




        #region Class
        public async Task<ClassViewModel> BindClassViewModelAsync(ClassViewModel model = null)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            if (model != null)
            {
                var Sections = (await _unitOfWork.ClassSection.FindSectionsInClassByClassIdAsync(model.Id, SchoolId))?.Sections.ToList();
                model.CBSections = (await _unitOfWork.SectionRepo.GetAllActiveAsync(SchoolId)).Select(s => new CheckBoxItem
                {
                    Id = s.Id,
                    Name = s.NormalizedName,
                    IsSelected = Sections != null && Sections.Any(a => a.Id == s.Id)
                }).ToList();
            }
            else
            {
                model = new ClassViewModel()
                {
                    CBSections = (await _unitOfWork.SectionRepo.GetAllActiveAsync(SchoolId)).Select(s => new CheckBoxItem
                    {
                        Id = s.Id,
                        Name = s.NormalizedName
                    }).ToList()
                };
            }
            return model;
        }
        public async Task<Response<List<ClassViewModel>>> GetListOfClassesAsync()
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            //var classes = await _unitOfWork.ClassRepo.GetAll_WithAddEditUserAsync(SchoolId);
            var classes = await _unitOfWork.ClassSection.GetAll_Class_Section_WithAddEditUserAsync(SchoolId);
            List<ClassViewModel> model = _mapper.Map<List<ClassViewModel>>(classes.ToList());
            return new Response<List<ClassViewModel>>(model);
        }
        public async Task<Response<ClassViewModel>> FindClassByIdAsync(int Id)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var classes = await _unitOfWork.ClassRepo.FindByIdAsync(Id, SchoolId);
            ClassViewModel model = _mapper.Map<ClassViewModel>(classes);
            var newModel = await BindClassViewModelAsync(model);
            return new Response<ClassViewModel>(newModel);
        }
        public async Task<Response<bool>> AddOrEditClassAsync(ClassViewModel model)
        {
            var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            Classes _class = _mapper.Map<Classes>(model);
            _class.NormalizedName = _class.Name.ToUpper().Trim();
            int result = 0;
            if (_class.Id <= 0)
            {
                _class.AddedBy = UserId;
                result = await _unitOfWork.ClassRepo.AddAsync(_class, SchoolId);
                model.Id = result;
            }
            else
            {
                _class.ModifyBy = UserId;
                result = await _unitOfWork.ClassRepo.UpdateAsync(_class, SchoolId);
            }
            if (result <= 0) return new Response<bool>("Error managing section");
            // Add sections 
            foreach (var cbi in model.CBSections)
            {
                bool isInClass = await _unitOfWork.ClassSection.IsSectionInClassAsync(model.Id, cbi.Id, SchoolId);
                if (cbi.IsSelected && !isInClass)
                {
                    result = await _unitOfWork.ClassSection.AddAsync(new ClassSection { ClassId = model.Id, SectionId = cbi.Id }, dbId: SchoolId);
                }
                else if (!cbi.IsSelected && isInClass)
                {
                    result = await _unitOfWork.ClassSection.DeleteAsync(ClassId: model.Id, SectionId: cbi.Id, dbId: SchoolId);
                }
                if (result <= 0) break;
            }
            if (result <= 0) return new Response<bool>("Error assigning section");
            return new Response<bool>((result > 0));
        }

        #endregion

        #region Section
        public async Task<Response<List<SectionViewModel>>> GetListOfSectionsAsync()
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var sections = await _unitOfWork.SectionRepo.GetAll_WithAddEditUserAsync(SchoolId);
            List<SectionViewModel> model = _mapper.Map<List<SectionViewModel>>(sections.ToList());
            return new Response<List<SectionViewModel>>(model);
        }
        public async Task<Response<SectionViewModel>> FindSectionByIdAsync(int Id)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var section = await _unitOfWork.SectionRepo.FindByIdAsync(Id, SchoolId);
            SectionViewModel model = _mapper.Map<SectionViewModel>(section);
            return new Response<SectionViewModel>(model);
        }
        public async Task<Response<bool>> AddOrEditSectionAsync(SectionViewModel model)
        {
            var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            Section section = _mapper.Map<Section>(model);
            section.NormalizedName = section.Name.ToUpper().Trim();
            var result = 0;
            if (section.Id <= 0)
            {
                section.AddedBy = UserId;
                result = await _unitOfWork.SectionRepo.AddAsync(section, SchoolId);
            }
            else
            {
                section.ModifyBy = UserId;
                result = await _unitOfWork.SectionRepo.UpdateAsync(section, SchoolId);
            }
            if (result <= 0) return new Response<bool>();
            return new Response<bool>((result > 0));
        }

        #endregion



        #region Session
        public async Task<Response<List<SessionViewModel>>> GetListOfSessionsAsync()
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var sessions = await _unitOfWork.Session.GetAll_WithAddEditUserAsync(SchoolId);
            List<SessionViewModel> model = _mapper.Map<List<SessionViewModel>>(sessions.ToList());
            return new Response<List<SessionViewModel>>(model);
        }
        public async Task<Response<SessionViewModel>> FindSessionByIdAsync(int Id)
        {
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            var session = await _unitOfWork.Session.FindByIdAsync(Id, SchoolId);
            SessionViewModel model = _mapper.Map<SessionViewModel>(session);
            return new Response<SessionViewModel>(model);
        }
        public async Task<Response<bool>> AddOrEditSessionAsync(SessionViewModel model)
        {
            var UserId = Convert.ToInt32(_loginService.GetLogedInSchoolAdminId());
            var SchoolId = _appContextAccessor.ClaimSchoolUniqueId();
            Session session = _mapper.Map<Session>(model);
            var result = 0;
            if (session.Id <= 0)
            {
                session.AddedBy = UserId;
                result = await _unitOfWork.Session.AddAsync(session, SchoolId);
            }
            else
            {
                session.ModifyBy = UserId;
                result = await _unitOfWork.Session.UpdateAsync(session, SchoolId);
            }
            if (result <= 0) return new Response<bool>();
            return new Response<bool>((result > 0));
        }

        #endregion

    }
}
