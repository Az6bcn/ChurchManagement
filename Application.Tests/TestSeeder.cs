using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Domain.Entities.PersonAggregate;
using Domain.Entities.TenantAggregate;
using Domain.Validators;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Shared.Enums;
using PersonManagementAggregate = Domain.Entities.PersonAggregate.PersonManagement;

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
            PersonManagementAggregate.CreateDepartment("Demo Department", tenant);

            context.ChangeTracker.Clear();
            
            context.Update(PersonManagementAggregate.Department);
            await TestDbCreator.SaveChangesAsync(context);

        }

        public static async Task CreateMember(ApplicationDbContext context, Tenant tenant)
        {
            var person = Person.Create
                (tenant.TenantId,
                 "Demo Member",
                 "Demo Surname",
                 "17/03",
                 "Male",
                 "+7703000000");

            PersonManagementAggregate.CreateMember(person, tenant, false);

            context.ChangeTracker.Clear();

            context.Update(PersonManagementAggregate.Member);
            await TestDbCreator.SaveChangesAsync(context);
        }
    }
}