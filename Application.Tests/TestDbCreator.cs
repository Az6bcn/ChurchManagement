using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Tests
{
    public class TestDbCreator
    {
        //creates a SQLite in-memory database and opens the connection to it.
        /// <summary>
        /// Delete existing db before creating a new one.
        /// </summary>
        /// <param name="builtServicesCollection"></param>
        /// <param name="dbContext"></param>
        public static void CreateDatabase(IServiceProvider builtServicesCollection,
                                          ApplicationTestDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Returns DbContext for test database
        /// </summary>
        /// <param name="builtServicesCollection"></param>
        /// <returns></returns>
        public static ApplicationTestDbContext GetApplicationTestDbContext(
            IServiceProvider builtServicesCollection)
            => builtServicesCollection.GetRequiredService<ApplicationTestDbContext>();


        public static void SaveChangesAndStopTracking(ApplicationTestDbContext dbContext)
        {
            dbContext.SaveChanges();

            dbContext.ChangeTracker.Clear();
        }
    }
}