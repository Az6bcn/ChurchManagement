using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Domain.Entities.TenantAggregate;
using Infrastructure.Persistence.Context;
using Shared.Enums;

namespace Application.Tests
{
    public class TestSeeder
    {
        public static async Task CreateDemoTenant(ApplicationDbContext context)
        {
            var demoTenant = Tenant.Create("Demo",
                                           string.Empty,
                                           CurrencyEnum.UsDollars,
                                           TenantStatusEnum.Pending);

            await context.AddAsync(demoTenant);
            await TestDbCreator.SaveChangesAsync(context);
        }
    }
}