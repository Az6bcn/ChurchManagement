using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class FinanceMap : IEntityTypeConfiguration<Finance>
    {
        public void Configure(EntityTypeBuilder<Finance> builder)
        {
            builder.ToTable("Finances");
            
            builder.HasQueryFilter(f => f.Deleted == null);

            // PK
            builder.HasKey(x => x.FinanceId)
                .HasName("PK_Finances_FinanceId")
                .IsClustered();

            // Columns
            builder.Property(t => t.FinanceId)
                .HasColumnName("FinanceId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(t => t.TenantId)
                .HasColumnName("TenantId")
                .HasColumnType("uuid")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(t => t.FinanceTypeId)
              .HasColumnName("FinanceTypeId")
              .HasColumnType("int")
              .ValueGeneratedNever()
              .IsRequired();

            builder.Property(t => t.ServiceTypeId)
              .HasColumnName("ServiceTypeId")
              .HasColumnType("int")
              .ValueGeneratedNever()
              .IsRequired();

            builder.Property(t => t.Amount)
              .HasColumnName("Amount")
              .HasColumnType("decimal(19, 3)")
              .ValueGeneratedNever()
              .IsRequired();

            builder.Property(t => t.CurrencyId)
              .HasColumnName("CurrencyId")
              .HasColumnType("int")
              .ValueGeneratedNever()
              .IsRequired();

            builder.Property(t => t.ServiceDate)
             .HasColumnName("ServiceDate")
             .HasColumnType("datetime")
             .ValueGeneratedNever();

            builder.Property(t => t.GivenDate)
             .HasColumnName("GivenDate")
             .HasColumnType("datetime")
             .ValueGeneratedNever()
             .IsRequired(false);

            builder.Property(t => t.Description)
              .HasColumnName("Description")
              .HasColumnType("varchar(max)")
              .IsUnicode(false)
              .ValueGeneratedNever()
              .IsRequired(false);

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

        }
    }
}
