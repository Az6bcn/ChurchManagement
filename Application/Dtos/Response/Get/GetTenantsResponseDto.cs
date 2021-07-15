using System;

namespace Application.Dtos.Response.Get
{
    public class GetTenantsResponseDto
    {
        public int TenantId { get; set; }
        public Guid TenantGuidId { get; set; }
        public string Name { get; set; }
        public int TenantStatus { get; set; }
        public int Currency { get; set; }
        public string LogoUrl { get; set; }
    }
}