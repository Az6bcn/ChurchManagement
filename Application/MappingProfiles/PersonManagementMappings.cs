using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Dtos.Response.Update;
using AutoMapper;
using Domain.Entities.PersonAggregate;
using Shared.Enums;

namespace Application.MappingProfiles
{
    public class PersonManagementMappings: Profile
    {
        public PersonManagementMappings()
        {
            CreateMap<Department, CreateDepartmentResponseDto>()
                .ForMember(dst => dst.DepartmentId,
                           options
                               => options.MapFrom(src => src.DepartmentId))
                .ForMember(dst => dst.TenantId,
                           options
                               => options.MapFrom(src => src.TenantId))
                .ForMember(dst => dst.Name,
                           options
                               => options.MapFrom(src => src.Name))
                .ReverseMap();
            
            CreateMap<Department, UpdateDepartmentResponseDto>()
                .ForMember(dst => dst.DepartmentId,
                           options
                               => options.MapFrom(src => src.DepartmentId))
                .ForMember(dst => dst.TenantId,
                           options
                               => options.MapFrom(src => src.TenantId))
                .ForMember(dst => dst.Name,
                           options
                               => options.MapFrom(src => src.Name))
                .ReverseMap();
            
            CreateMap<Member, CreateMemberResponseDto>()
                .ForMember(dst => dst.MemberId,
                           options
                               => options.MapFrom(src => src.MemberId))
                .ForMember(dst => dst.Surname,
                           options
                               => options.MapFrom(src => src.Surname))
                .ForMember(dst => dst.Name,
                           options
                               => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.Gender,
                           options
                               => options.MapFrom(src => src.Gender))
                .ForMember(dst => dst.PhoneNumber,
                           options
                               => options.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.DateAndMonthOfBirth,
                           options
                               => options.MapFrom(src => src.DateMonthOfBirth))
                .ForMember(dst => dst.IsWorker,
                           options
                               => options.MapFrom(src => src.IsWorker))
                .ReverseMap();

            CreateMap<Member, UpdateMemberResponseDto>()
                .ForMember(dst => dst.MemberId,
                           options
                               => options.MapFrom(src => src.MemberId))
                .ForMember(dst => dst.Surname,
                           options
                               => options.MapFrom(src => src.Surname))
                .ForMember(dst => dst.Name,
                           options
                               => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.Gender,
                           options
                               => options.MapFrom(src => src.Gender))
                .ForMember(dst => dst.PhoneNumber,
                           options
                               => options.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.DateAndMonthOfBirth,
                           options
                               => options.MapFrom(src => src.DateMonthOfBirth))
                .ForMember(dst => dst.IsWorker,
                           options
                               => options.MapFrom(src => src.IsWorker))
                .ReverseMap();
            
            CreateMap<NewComer, CreateNewComerResponseDto>()
                .ForMember(dst => dst.NewComerId,
                           options
                               => options.MapFrom(src => src.NewComerId))
                .ForMember(dst => dst.Surname,
                           options
                               => options.MapFrom(src => src.Surname))
                .ForMember(dst => dst.Name,
                           options
                               => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.Gender,
                           options
                               => options.MapFrom(src => src.Gender))
                .ForMember(dst => dst.PhoneNumber,
                           options
                               => options.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.DateAndMonthOfBirth,
                           options
                               => options.MapFrom(src => src.DateMonthOfBirth))
                .ForMember(dst => dst.DateAttended,
                           options
                               => options.MapFrom(src => src.DateAttended))
                .ForMember(dst => dst.ServiceType,
                           options
                               => options.MapFrom(src => (ServiceEnum) src.ServiceTypeId))
                .ReverseMap();
            
            CreateMap<NewComer, UpdateNewComerResponseDto>()
                .ForMember(dst => dst.NewComerId,
                           options
                               => options.MapFrom(src => src.NewComerId))
                .ForMember(dst => dst.Surname,
                           options
                               => options.MapFrom(src => src.Surname))
                .ForMember(dst => dst.Name,
                           options
                               => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.Gender,
                           options
                               => options.MapFrom(src => src.Gender))
                .ForMember(dst => dst.PhoneNumber,
                           options
                               => options.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.DateAndMonthOfBirth,
                           options
                               => options.MapFrom(src => src.DateMonthOfBirth))
                .ForMember(dst => dst.DateAttended,
                           options
                               => options.MapFrom(src => src.DateAttended))
                .ForMember(dst => dst.ServiceTypeEnum,
                           options
                               => options.MapFrom(src => (ServiceEnum) src.ServiceTypeId))
                .ReverseMap();
            
            CreateMap<Minister, CreateMinisterResponseDto>()
                .ForMember(dst => dst.MinisterId,
                           options
                               => options.MapFrom(src => src.MinisterId))
                .ForMember(dst => dst.MemberId,
                           options
                               => options.MapFrom(src => src.MemberId))
                .ForMember(dst => dst.MinisterTitle,
                           options
                               => options.MapFrom(src => (MinisterTitleEnum)src.MinisterTitleId))
                .ReverseMap();
        }
    }
}