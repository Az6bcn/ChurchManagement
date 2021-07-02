using System.Data.Common;
using Domain.Entities.TenantAggregate;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Tests
{
    public class ApplicationTestDbContext : DbContext
    {
        private readonly DbConnection _connection;
        
        public ApplicationTestDbContext(DbContextOptions options) 
            : base(options)
        {
            _connection = RelationalOptionsExtension.Extract(options).Connection;
        }

        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TenantMap());
            modelBuilder.ApplyConfiguration(new MemberMap());
            modelBuilder.ApplyConfiguration(new DepartmentMap());
            modelBuilder.ApplyConfiguration(new FinanceMap());
            modelBuilder.ApplyConfiguration(new FinanceTypeMap());
            modelBuilder.ApplyConfiguration(new TenantStatusMap());
            modelBuilder.ApplyConfiguration(new NewComerMap());
            modelBuilder.ApplyConfiguration(new AttendanceMap());
            modelBuilder.ApplyConfiguration(new MinisterMap());
        }
        
        public override void Dispose()
        {
            _connection.Dispose();
            base.Dispose();
        }
    }
}