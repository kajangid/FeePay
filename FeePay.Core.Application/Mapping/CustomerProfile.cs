using AutoMapper;
using FeePay.Core.Application.DTOs;
using FeePay.Core.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
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
                .ForMember(dest => dest.ModifyById, opt => opt.MapFrom(src => (src.ModifyByUser != null ? src.ModifyByUser.Id : 0)));


            // Mapping properties from StaffMemberViewModel to SchoolAdminUser 
            CreateMap<StaffMemberViewModel, SchoolAdminUser>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => (src.FirstName + " " + src.LastName)));
        }
    }
}
