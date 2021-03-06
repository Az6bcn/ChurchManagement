using System;
using System.Threading.Tasks;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

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
        public static void CreateDatabase(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Returns DbContext for test database
        /// </summary>
        /// <param name="builtServicesCollection"></param>
        /// <returns></returns>
        public static ApplicationDbContext GetApplicationTestDbContext(IServiceProvider builtServicesCollection)
            => builtServicesCollection.GetRequiredService<ApplicationDbContext>();


        public static void SaveChangesAndStopTracking(ApplicationDbContext dbContext)
        {
            dbContext.SaveChanges();

            dbContext.ChangeTracker.Clear();
        }
        
        public static async Task SaveChangesAndStopTrackingAsync(ApplicationDbContext dbContext)
        {
            await dbContext.SaveChangesAsync();

            dbContext.ChangeTracker.Clear();
        }
        
        public static async Task SaveChangesAsync(ApplicationDbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }
    }
}