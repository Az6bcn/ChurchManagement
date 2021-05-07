using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class ServiceTypeMap : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.ToTable("ServiceTypes");

            // PK
            builder.HasKey(x => x.ServiceTypeId)
                .HasName("PK_ServiceTypes_ServiceTypeId")
                .IsClustered();

            // Columns
            builder.Property(t => t.ServiceTypeId)
                .HasColumnName("ServiceTypeId")
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
                ServiceType.Create(1, "Thanksgiving"),
                ServiceType.Create(2, "Mid Week Service"),
                ServiceType.Create(3, "Sunday Service"),
                ServiceType.Create(4, "Crusade"),
                ServiceType.Create(5, "Cross Over"),
                ServiceType.Create(6, "Others")
                );
        }
    }
}
