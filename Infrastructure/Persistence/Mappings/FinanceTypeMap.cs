using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class FinanceTypeMap : IEntityTypeConfiguration<FinanceType>
    {
        public void Configure(EntityTypeBuilder<FinanceType> builder)
        {
            builder.ToTable("FinanceTypes");

            // PK
            builder.HasKey(x => x.FinanceTypeId)
                .HasName("PK_FinanceTypes_FinanceTypeId")
                .IsClustered();

            // Columns
            builder.Property(t => t.FinanceTypeId)
                .HasColumnName("FinanceTypeId")
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
                FinanceType.CreateFinanceType(1, "Thanksgiving"),
                FinanceType.CreateFinanceType(2, "Offering"),
                FinanceType.CreateFinanceType(3, "Spending"),
                FinanceType.CreateFinanceType(4, "Donation"),
                FinanceType.CreateFinanceType(5, "Tithe"),
                FinanceType.CreateFinanceType(6, "Mid Week Service Offering"),
                FinanceType.CreateFinanceType(7, "Others")
                );
        }
    }
}
