using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class CurrencyMap : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");

            // PK
            builder.HasKey(x => x.CurrencyId)
                .HasName("PK_Currencies_CurrencyId")
                .IsClustered();

            // Columns
            builder.Property(t => t.CurrencyId)
                .HasColumnName("CurrencyId")
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
                Currency.CreateCurrency(1, "Naira"),
                Currency.CreateCurrency(2, "British Pounds"),
                Currency.CreateCurrency(3, "US Dollars")
                );
        }
    }
}
