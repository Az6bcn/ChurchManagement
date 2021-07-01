using Domain.Entities.FinanceAggregate;
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
                FinanceType.Create(1, "Thanksgiving"),
                FinanceType.Create(2, "Offering"),
                FinanceType.Create(3, "Spending"),
                FinanceType.Create(4, "Donation"),
                FinanceType.Create(5, "Tithe"),
                FinanceType.Create(6, "Mid Week Service Offering"),
                FinanceType.Create(7, "Others")
                );
        }
    }
}
