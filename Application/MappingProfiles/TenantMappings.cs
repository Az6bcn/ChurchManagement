using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using AutoMapper;
using Domain.Entities.TenantAggregate;
using Shared.Enums;

namespace Application.MappingProfiles
{
    public class TenantMappings: Profile
    {
        public TenantMappings()
        {
            CreateMap<Tenant, CreateTenantResponseDto>()
                .ForMember(dst => dst.TenantStatusEnum, 
                           options 
                               => options.MapFrom(src => src.TenantId))
                .ForMember(dst => dst.Name, 
                           options 
                               => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.LogoUrl, options 
                               => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.TenantStatusEnum, options 
                               => options.MapFrom(src => (TenantStatusEnum)src.TenantStatusId))
                .ForMember(dst => dst.CurrencyEnum, options 
                               => options.MapFrom(src => (CurrencyEnum)src.CurrencyId))
                .ReverseMap();

        }
    }
}