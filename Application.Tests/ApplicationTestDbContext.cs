// using System.Data.Common;
// using Domain.Entities.TenantAggregate;
// using Domain.ValueObjects;
// using Infrastructure.Persistence.Context;
// using Infrastructure.Persistence.Mappings;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Infrastructure;
//
// namespace Application.Tests
// {
//     public class ApplicationTestDbContext : ApplicationDbContext
//     {
//         private readonly DbConnection _connection;
//         
//         public ApplicationTestDbContext(DbContextOptions<ApplicationDbContext> options) 
//             : base(options)
//         {
//             _connection = RelationalOptionsExtension.Extract(options).Connection;
//         }
//
//         public DbSet<Tenant> Tenants { get; set; }
//
//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             base.OnModelCreating(modelBuilder);
//
//             modelBuilder.ApplyConfiguration(new TenantMap());
//             modelBuilder.ApplyConfiguration(new MemberMap());
//             modelBuilder.ApplyConfiguration(new DepartmentMap());
//             modelBuilder.ApplyConfiguration(new FinanceMap());
//             modelBuilder.ApplyConfiguration(new FinanceTypeMap());
//             modelBuilder.ApplyConfiguration(new NewComerMap());
//             modelBuilder.ApplyConfiguration(new AttendanceMap());
//             modelBuilder.ApplyConfiguration(new MinisterMap());
//             
//             // Enities to ignore
//             modelBuilder.Ignore<Person>();
//             modelBuilder.Ignore<Currency>();
//             modelBuilder.Ignore<FinanceType>();
//             modelBuilder.Ignore<ServiceType>();
//             modelBuilder.Ignore<MinisterTitle>();
//             
//         }
//         
//         public override void Dispose()
//         {
//             _connection.Dispose();
//             base.Dispose();
//         }
//     }
// }