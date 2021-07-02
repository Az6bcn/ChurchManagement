using System;
using System.Data.Common;
using Infrastructure;
using Infrastructure.Persistence.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Tests
{
    public class TestDependenciesResolver
    {
        public static IServiceCollection AddServices()
        {
            var services = new ServiceCollection();
            
            // Add in memory sqlite db for test
            AddSqlLiteTestDb(services);
            
            // Add all dependencies
            services.AddApplicationServices();
            services.AddInfrastructureServices();
            services.AddTransient<ApplicationDbContext>();

            return services;
        }
        
        private static IServiceCollection AddSqlLiteTestDb(IServiceCollection services)
        {
            //var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationTestDbContext>(options =>
            {
                options.UseSqlite(CreateInMemoryDatabase())
                       .EnableSensitiveDataLogging()
                       .LogTo(Console.WriteLine, LogLevel.Information);
            });

            return services;
        }
        
        /// <summary>
        /// Creates a SQLite in-memory database and opens the connection to it.
        /// </summary>
        /// <returns></returns>
        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public static IServiceProvider BuildServices(IServiceCollection services)
            => services.BuildServiceProvider();

        /// <summary>
        /// Returns object for the resolved service type T.
        /// </summary>
        /// <param name="builtServiceCollection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>(IServiceProvider builtServiceCollection)
            => builtServiceCollection.GetRequiredService<T>();

    }
}