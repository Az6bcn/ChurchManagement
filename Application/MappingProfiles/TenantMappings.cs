using Application.Dtos.Request.Create;
using Application.Dtos.Response.Create;
using Application.Dtos.Response.Update;
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
                .ForMember(dst => dst.TenantId, 
                           options 
                               => options.MapFrom(src => src.TenantId))
                .ForMember(dst => dst.Name, 
                           options 
                               => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.LogoUrl, options 
                               => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.TenantStatus, options 
                               => options.MapFrom(src => (TenantStatusEnum)src.TenantStatusId))
                .ForMember(dst => dst.Currency, options 
                               => options.MapFrom(src => (CurrencyEnum)src.CurrencyId))
                .ReverseMap();

            CreateMap<Tenant, UpdateTenantResponseDto>();
            // .ForMember(dst => dst.TenantId, 
            //            options 
            //                => options.MapFrom(src => src.TenantId))
            // .ForMember(dst => dst.Name, 
            //            options 
            //                => options.MapFrom(src => src.Name))
            // .ForMember(dst => dst.LogoUrl, options 
            //                => options.MapFrom(src => src.Name))
            // .ForMember(dst => dst.TenantStatusEnum, options 
            //                => options.MapFrom(src => (TenantStatusEnum)src.TenantStatusId))
            // .ForMember(dst => dst.CurrencyEnum, options 
            //                => options.MapFrom(src => (CurrencyEnum)src.CurrencyId))
            // .ReverseMap();

        }
    }
}