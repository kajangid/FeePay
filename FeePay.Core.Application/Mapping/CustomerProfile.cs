using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Domain.Entities.Identity;
using FeePay.Core.Domain.Entities.School;
using FeePay.Core.Domain.Entities.Student;
using FeePay.Core.Domain.Entities.SuperAdmin;
using FeePay.Core.Application.IoC;

namespace FeePay.Core.Application.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            #region School Identity

            // Mapping properties from SchoolAdminRole to RoleViewModel 
            CreateMap<SchoolAdminRole, RoleViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)));


            // Mapping properties from RoleViewModel to SchoolAdminRole 
            CreateMap<RoleViewModel, SchoolAdminRole>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()))
                .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpper().Trim()));


            // Mapping properties from SchoolAdminUser to StaffMemberViewModel 
            CreateMap<SchoolAdminUser, StaffMemberViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)))
                .ForMember(dest => dest.RoleListString, opt => opt.MapFrom(src => (src.Roles != null && src.Roles.Count > 0 ? string.Join(",", src.Roles.Select(s => s.Name).ToArray()) : "")));


            // Mapping properties from StaffMemberViewModel to SchoolAdminUser 
            CreateMap<StaffMemberViewModel, SchoolAdminUser>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => (src.FirstName + " " + src.LastName)));

            #endregion

            #region Fee Management

            // Mapping properties from FeeType to FeeTypeViewModel 
            CreateMap<FeeType, FeeTypeViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)));

            // Mapping properties from FeeTypeViewModel to FeeType 
            CreateMap<FeeTypeViewModel, FeeType>();



            // Mapping properties from FeeGroup to FeeGroupViewModel 
            CreateMap<FeeGroup, FeeGroupViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)))
                .ForMember(dest => dest.FeeType, opt => opt.MapFrom(src => src.FeeTypeList))
                .ForMember(dest => dest.FeeMaster, opt => opt.MapFrom(src => src.FeeMasterList));


            // Mapping properties from FeeGroupViewModel to FeeGroup 
            CreateMap<FeeGroupViewModel, FeeGroup>()
                .ForMember(dest => dest.FeeTypeList, opt => opt.MapFrom(src => src.FeeType))
                .ForMember(dest => dest.FeeMasterList, opt => opt.MapFrom(src => src.FeeMaster));



            // Mapping properties from FeeMaster to FeeMasterViewModel 
            CreateMap<FeeMaster, FeeMasterViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)));


            // Mapping properties from FeeMasterViewModel to FeeMaster 
            CreateMap<FeeMasterViewModel, FeeMaster>();

            #endregion

            #region Academics

            // Mapping properties from Classes to ClassViewModel 
            CreateMap<Classes, ClassViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)));


            // Mapping properties from ClassViewModel to Classes 
            CreateMap<ClassViewModel, Classes>();

            // Mapping properties from Section to SectionViewModel 
            CreateMap<Section, SectionViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)));


            // Mapping properties from SectionViewModel to Section 
            CreateMap<SectionViewModel, Section>();

            // Mapping properties from Section to SessionViewModel 
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)));


            // Mapping properties from SessionViewModel to Section 
            CreateMap<SessionViewModel, Session>();
            #endregion

            #region School Management
            // Mapping properties from SchoolAdminUser to UserProfileViewModel  
            CreateMap<SchoolAdminUser, UserProfileViewModel>();
            CreateMap<SchoolAdminUser, UserPasswordViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<StudentLogin, UserPasswordViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            #endregion

            #region Student 

            // Mapping properties from StudentAdmission to StudentAdmissionViewModel 
            CreateMap<StudentAdmission, StudentAdmissionViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)))
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));


            // Mapping properties from StudentAdmissionViewModel to StudentAdmission 
            CreateMap<StudentAdmissionViewModel, StudentAdmission>();


            CreateMap<StudentFees, StudentFeesViewModel>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Id.EncryptID()));
            CreateMap<StudentFeesViewModel, StudentFees>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Token.DecryptID()));


            #endregion

            #region Super Admin Identity

            //// Mapping properties from SchoolAdminRole to RoleViewModel 
            //CreateMap<SuperAdminRole, RoleViewModel>()
            //    .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
            //    .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
            //    .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
            //    .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)));


            //// Mapping properties from RoleViewModel to SchoolAdminRole 
            //CreateMap<RoleViewModel, SchoolAdminRole>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim()))
            //    .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpper().Trim()));


            //// Mapping properties from SchoolAdminUser to StaffMemberViewModel 
            //CreateMap<SuperAdminUser, StaffMemberViewModel>()
            //    .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
            //    .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
            //    .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
            //    .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)))
            //    .ForMember(dest => dest.RoleListString, opt => opt.MapFrom(src => (src.Roles != null && src.Roles.Count > 0 ? string.Join(",", src.Roles.Select(s => s.Name).ToArray()) : "")));


            //// Mapping properties from StaffMemberViewModel to SchoolAdminUser 
            //CreateMap<StaffMemberViewModel, SuperAdminUser>()
            //    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => (src.FirstName + " " + src.LastName)));

            #endregion

            #region SUPER ADMIN
            // Mapping properties from SuperAdminUser to SuperAdmin_UserViewModel 
            CreateMap<SuperAdminUser, SuperAdmin_UserViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)));
            // Mapping properties from SuperAdmin_UserViewModel to SuperAdminUser 
            CreateMap<SuperAdmin_UserViewModel, SuperAdminUser>();

            // Mapping properties from StudentAdmission to StudentAdmissionViewModel 
            CreateMap<RegisteredSchool, RegisterSchoolViewModel>()
                .ForMember(dest => dest.AddedBy, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Email : string.Empty)))
                .ForMember(dest => dest.ModifyBy, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Email : string.Empty)))
                .ForMember(dest => dest.AddedById, opt => opt.MapFrom(src => (src.AddedByUser != null ? src.AddedByUser.Id : 0)))
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)));
            // Mapping properties from StudentAdmissionViewModel to StudentAdmission 
            CreateMap<RegisterSchoolViewModel, RegisteredSchool>();

            // Mapping properties from SuperAdminUser to UserPasswordViewModel 
            CreateMap<SuperAdminUser, UserPasswordViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            #endregion
        }
    }
}
