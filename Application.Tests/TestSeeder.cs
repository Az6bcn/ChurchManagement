using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Domain.Entities.TenantAggregate;
using Domain.Validators;
using Infrastructure.Persistence.Context;
using Shared.Enums;

namespace Application.Tests
{
    public class TestSeeder
    {
        public static async Task CreateDemoTenant(ApplicationDbContext context,
                                                  IValidateTenantInDomain domainValidator)
        {
            var demoTenant = Tenant.Create("Demo",
                                           string.Empty,
                                           CurrencyEnum.UsDollars,
                                           domainValidator,
                                           out IDictionary<string, object> errors);

            await context.AddAsync(demoTenant);
            await TestDbCreator.SaveChangesAsync(context);
        }
    }
}