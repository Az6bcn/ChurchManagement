using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class TenantStatusMap: IEntityTypeConfiguration<TenantStatus>
    {
        public void Configure(EntityTypeBuilder<TenantStatus> builder)
        {
            builder.ToTable("TenantStatus");

            // PK
            builder.HasKey(x => x.TenantStatusId)
                .HasName("PK_TenantStatus_TenantStatusId")
                .IsClustered();

            // Columns
            builder.Property(t => t.TenantStatusId)
                .HasColumnName("TenantStatusId")
                .HasColumnType("int")
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

            // seed
            builder.HasData(
                TenantStatus.Create(1, "Active"),
                TenantStatus.Create(2, "On Hold"),
                TenantStatus.Create(3, "Suspended"),
                TenantStatus.Create(4, "Pending"),
                TenantStatus.Create(5, "Cancelled")
            );
        }
    }
}