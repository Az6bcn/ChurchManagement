using Domain.Entities.TenantAggregate;
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
                //.HasColumnType("int")
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
                .HasColumnType("varchar(255)")
                .IsUnicode(false)
                .HasMaxLength(255)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(t => t.LogoUrl)
              .HasColumnName("LogoUrl")
              //.HasColumnType("varchar(max)")
              .IsUnicode(false)
              .ValueGeneratedNever()
              .IsRequired(false);

            builder.Property(t => t.CurrencyId)
              .HasColumnName("CurrencyId")
              //.HasColumnType("int")
              .ValueGeneratedNever()
              .IsRequired();

            builder.Property(t => t.TenantStatusId)
             .HasColumnName("TenantStatusId")
             //.HasColumnType("int")
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
            
            //Ignores :
            // Navigation Properties to Ignore in tenant(Not Mapped)
            //builder.Ignore(x => x.Currency);

            // Relationships and Foreign Key Constraints
            // builder.HasOne(t => t.Currency)
            //     .WithMany(c => c.Tenants)
            //     .HasForeignKey(t => t.CurrencyId)
            //     .HasConstraintName("FK_Tenants_Currencies_CurrencyId")
            //     .OnDelete(DeleteBehavior.Restrict);
            //
            // builder.HasMany(t => t.Finances)
            //        .WithOne(f => f.Tenant)
            //        .HasForeignKey(m => m.TenantId)
            //        .HasConstraintName("FK_Tenants_Finances_TenantId")
            //        .OnDelete(DeleteBehavior.Restrict);
            // builder.HasOne(t => t.TenantStatus)
            //     .WithMany(ts => ts.Tenants)
            //     .HasForeignKey(t => t.TenantStausId)
            //     .HasConstraintName("FK_Tenants_TenantStatuses_TenantStatusId")
            //     .OnDelete(DeleteBehavior.Restrict);
            //
            // builder.HasMany(t => t.Members)
            //     .WithOne(m => m.Tenant)
            //     .HasForeignKey(m => m.TenantId)
            //     .HasConstraintName("FK_Tenants_Members_TenantId")
            //     .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(t => t.TenantStatus)
                .WithMany()
                .HasForeignKey(t => t.TenantStatusId)
                .HasConstraintName("FK_TenantsTenantStatusId_TenantStatuses_TenantStatusId")
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes and Constraints
            // UQ
            builder.HasIndex(uq => new {uq.TenantGuidId, uq.Name })
                .HasDatabaseName("UQ_Tenants_TenantGuidId_Name");

            builder.HasIndex(uq => uq.TenantGuidId)
                   .HasDatabaseName("UQ_Tenants_TenantGuidId");
            // CK
            builder.HasCheckConstraint("CK_Tenant_Name", "Name IS NOT NULL");
        }
    }
}
