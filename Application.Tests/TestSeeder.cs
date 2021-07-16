using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Domain.Entities.PersonAggregate;
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

        public static async Task CreateDemoDepartment(ApplicationDbContext context,
                                                      Tenant tenant)
        {
            var demoDepartment = Department.Create("Demo Department", tenant);

            await context.AddAsync(demoDepartment);
            await TestDbCreator.SaveChangesAsync(context);
        }
    }
}