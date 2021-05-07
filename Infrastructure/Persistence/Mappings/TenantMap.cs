using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class TenantMap : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");

            builder.HasQueryFilter(t => t.Deleted == null);

            // PK
            builder.HasKey(x => x.TenantId)
                .HasName("PK_Tenants_TenantId")
                .IsClustered();

            // Columns
            builder.Property(t => t.TenantId)
                .HasColumnName("TenantId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .IsRequired();
            
            builder.Property(t => t.TenantGuidId)
                .HasColumnName("TenantGuidId")
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("newid()")
                .IsRequired();

            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(200)")
                .IsUnicode(false)
                .HasMaxLength(200)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(t => t.LogoUrl)
              .HasColumnName("LogoUrl")
              .HasColumnType("varchar(max)")
              .IsUnicode(false)
              .ValueGeneratedNever()
              .IsRequired(false);

            builder.Property(t => t.CurrencyId)
              .HasColumnName("CurrencyId")
              .HasColumnType("int")
              .ValueGeneratedNever()
              .IsRequired();

            builder.Property(t => t.TenantStausId)
             .HasColumnName("TenantStatusId")
             .HasColumnType("int")
             .ValueGeneratedNever()
             .IsRequired();

            builder.Property(t => t.CreatedAt)
             .HasColumnName("CreatedAt")
             .HasColumnType("datetime")
             .ValueGeneratedNever()
             .IsRequired();

            builder.Property(t => t.UpdatedAt)
             .HasColumnName("UpdatedAt")
             .HasColumnType("datetime")
             .ValueGeneratedNever()
             .HasDefaultValueSql("null")
             .IsRequired(false);

            builder.Property(t => t.Deleted)
             .HasColumnName("Deleted")
             .HasColumnType("datetime")
             .ValueGeneratedNever()
             .HasDefaultValueSql("null")
             .IsRequired(false);

            // Relationships and Foreign Key Constraints
            builder.HasOne(t => t.Currency)
                .WithMany(c => c.Tenants)
                .HasForeignKey(t => t.CurrencyId)
                .HasConstraintName("FK_Tenants_Currencies_CurrencyId")
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(t => t.TenantStatus)
                .WithMany(ts => ts.Tenants)
                .HasForeignKey(t => t.TenantStausId)
                .HasConstraintName("FK_Tenants_TenantStatuses_TenantStatusId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Members)
                .WithOne(m => m.Tenant)
                .HasForeignKey(m => m.TenantId)
                .HasConstraintName("FK_Tenants_Members_TenantId")
                .OnDelete(DeleteBehavior.Restrict);
            

            builder.HasMany(t => t.Finances)
               .WithOne(f => f.Tenant)
               .HasForeignKey(m => m.TenantId)
               .HasConstraintName("FK_Tenants_Finances_TenantId")
               .OnDelete(DeleteBehavior.Restrict);

            // Indexes and Contraints
            // UQ
            builder.HasIndex(uq => new {uq.TenantGuidId, uq.Name })
                .HasDatabaseName("UQ_TenantGuidId_TenantName");
            
            builder.HasIndex(uq => uq.TenantGuidId)
                .HasDatabaseName("UQ_TenantGuidId");
        }
    }
}
