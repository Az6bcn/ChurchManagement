using Domain.Entities.FinanceAggregate;
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
                //.HasColumnType("int")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(t => t.TenantId)
                .HasColumnName("TenantId")
                //.HasColumnType("int")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(t => t.FinanceTypeId)
              .HasColumnName("FinanceTypeId")
              //.HasColumnType("int")
              .ValueGeneratedNever()
              .IsRequired();

            builder.Property(t => t.ServiceTypeId)
              .HasColumnName("ServiceTypeId")
              //.HasColumnType("int")
              .ValueGeneratedNever()
              .IsRequired();

            builder.Property(t => t.Amount)
              .HasColumnName("Amount")
              .HasColumnType("decimal(19, 3)")
              .ValueGeneratedNever()
              .IsRequired();

            builder.Property(t => t.CurrencyId)
              .HasColumnName("CurrencyId")
              //.HasColumnType("int")
              .ValueGeneratedNever()
              .IsRequired();

            // builder.Property(t => t.ServiceDate)
            //  .HasColumnName("ServiceDate")
            //  .HasColumnType("datetime")
            //  .HasDefaultValueSql("null")
            //  .ValueGeneratedNever();

            builder.Property(t => t.GivenDate)
             .HasColumnName("GivenDate")
             .HasColumnType("date")
             .ValueGeneratedNever()
             .HasDefaultValueSql("null")
             .IsRequired();

            builder.Property(t => t.Description)
              .HasColumnName("Description")
              //.HasColumnType("varchar(max)")
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
             .HasDefaultValueSql("null")
             .IsRequired(false);

            builder.Property(t => t.Deleted)
             .HasColumnName("Deleted")
             .HasColumnType("datetime")
             .ValueGeneratedNever()
             .HasDefaultValueSql("null")
             .IsRequired(false);
            
            
            // Relationships and Foreign Key Constraints
            builder.HasOne(x => x.Tenant)
                   .WithOne()
                   .HasForeignKey<Finance>(x => x.TenantId)
                   .HasConstraintName("FK_Finances_TenantId_Tenants_TenantId")
                   .OnDelete(DeleteBehavior.Restrict);
            
            // Indexes and Constraints
            // TODO : -- CK Ammount > 0, CurrencyId > 0, TenantId > 0

        }
    }
}
