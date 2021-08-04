using System;
using System.Threading.Tasks;
using Application.Dtos.Request.Create;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests.Commands.Finance
{
    public class FinanceCreationTests
    {
        private readonly IServiceProvider _builtServices;

        public FinanceCreationTests()
        {
            var services = GetServices();
            _builtServices = TestDependenciesResolver.BuildServices(services);
        }
        
        private IServiceCollection GetServices() => TestDependenciesResolver.AddServices();


        public async Task ExecuteAsync_WhenCalled_CreateFinanceInDatabase()
        {
            var dbContext = TestDependenciesResolver.GetService<ApplicationDbContext>(_builtServices);
            var target = TestDependenciesResolver.GetService<ICreateFinanceCommand>(_builtServices);

            var request = new CreateFinanceRequestDto
            {

            };
        }
    }
}