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
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
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

            builder.Property(t => t.IsActive)
             .HasColumnName("IsActive")
             .HasColumnType("bit")
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
             .IsRequired(false);

            builder.Property(t => t.Deleted)
             .HasColumnName("Deleted")
             .HasColumnType("datetime")
             .ValueGeneratedNever()
             .IsRequired(false);

            // Relationships and Foreign Key Constraints
            builder.HasOne(t => t.Currency)
                .WithOne(c => c.Tenant)
                .HasForeignKey<Tenant>(t => t.CurrencyId)
                .HasConstraintName("FK_Tenants_Currencies_CurrencytypeId")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(t => t.Members)
                .WithOne(m => m.Tenant)
                .HasForeignKey(m => m.TenantId)
                .HasConstraintName("FK_Tenants_Members_TenantId")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(t => t.Finances)
               .WithOne(f => f.Tenant)
               .HasForeignKey(m => m.TenantId)
               .HasConstraintName("FK_Tenants_Finances_TenantId")
               .OnDelete(DeleteBehavior.ClientSetNull);

            // Indexes and Contraints
            // UQ
            builder.HasIndex(uq => new { uq.TenantId, uq.Name })
                .HasDatabaseName("UQ_TenantId_TenantName");
        }
    }
}
