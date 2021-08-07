using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITenantRepositoryAsync _tenantRepo;

        public TenantMiddleware(RequestDelegate next
                                )
        {
            _next = next;
            
        }

        public async Task InvokeAsync(HttpContext context, ITenantRepositoryAsync _tenantRepo)
        {
            var tenantKey  = context.Request.Headers["Api-Key"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(tenantKey))
            {
                var tenant = await _tenantRepo.GetTenantByGuidIdAsync(Guid.Parse(tenantKey));

                context.Items.TryAdd("tenantId", tenant.TenantId);
            }
                
            // call next middleware in the pipeline
            await _next(context);
        }
    }
}