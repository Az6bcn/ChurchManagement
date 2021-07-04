using Domain.Entities.TenantAggregate;
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
                   //.HasColumnType("int")
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

        }
    }
}