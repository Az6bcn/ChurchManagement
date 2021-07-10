using System;
using Application.Dtos;
using AutoMapper;
using Domain.ProjectionEntities;

namespace Application.MappingProfiles
{
    public class DashboardObjectMappings : Profile
    {
        public DashboardObjectMappings()
        {
            // CreateMap<TenantDetailsProjection, TenantDetailsDto>()
            // .ForMember(dest => dest.Name,
            //     opt => opt.MapFrom(src => src.Name))
            // .ForMember(dest => dest.LogoUrl,
            //     opt => opt.MapFrom(src => src.LogoUrl));
        }
    }
}
