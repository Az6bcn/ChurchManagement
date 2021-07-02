using Domain.Entities.TenantAggregate;
using Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
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
            //modelBuilder.ApplyConfiguration(new ServiceTypeMap());
            //modelBuilder.ApplyConfiguration(new MinisterTitleMap());
            modelBuilder.ApplyConfiguration(new TenantStatusMap());
            modelBuilder.ApplyConfiguration(new NewComerMap());
            modelBuilder.ApplyConfiguration(new AttendanceMap());
            modelBuilder.ApplyConfiguration(new MinisterMap());
            //modelBuilder.ApplyConfiguration(new CurrencyMap());
        }
    }
}

