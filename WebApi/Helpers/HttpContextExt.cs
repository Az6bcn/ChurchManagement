using Microsoft.AspNetCore.Http;

namespace WebApi.Helpers
{
    public static class HttpContextExt
    {
        public static int GetTenantId(this HttpContext context)
        {
            if(context.Items.TryGetValue("tenantId", out var value))
                return (int)value;

            return default;
        }
        
        public static string? GetTenantCurrencyCode(this HttpContext context)
        {
            if(context.Items.TryGetValue("tenantCurrencyCode", out var value))
                return (string)value;

            return default;
        }
    }
}