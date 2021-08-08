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
    }
}