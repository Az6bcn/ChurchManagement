using Domain.Entities.PersonAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings;

public class NewComerMap: IEntityTypeConfiguration<NewComer>
{
    public void Configure(EntityTypeBuilder<NewComer> builder)
    {
        builder.ToTable("NewComers");

        builder.HasQueryFilter(m => m.Deleted == null);

        //PK
        builder.HasKey(m => m.NewComerId)
               .HasName("PK_NewComers_NewComerId")
               .IsClustered();

        // Columns
        builder.Property(m => m.NewComerId)
               .HasColumnName("NewComerId")
               //.HasColumnType("int")
               .UseIdentityColumn()
               .ValueGeneratedOnAdd()
               .IsRequired();

        builder.Property(t => t.TenantId)
               .HasColumnName("TenantId")
               //.HasColumnType("int")
               .ValueGeneratedNever()
               .IsRequired();
            
        builder.Property(t => t.ServiceTypeId)
               .HasColumnName("ServiceTypeId")
               //.HasColumnType("int")
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(t => t.Name)
               .HasColumnName("Name")
               .HasColumnType("varchar(200)")
               .IsUnicode(false)
               .HasMaxLength(200)
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(t => t.Surname)
               .HasColumnName("Surname")
               .HasColumnType("varchar(200)")
               .IsUnicode(false)
               .HasMaxLength(200)
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(t => t.DateMonthOfBirth)
               .HasColumnName("DateMonthOfBirth")
               .HasColumnType("varchar(50)")
               .IsUnicode(false)
               .HasMaxLength(50)
               .ValueGeneratedNever()
               .IsRequired();
            
        builder.Property(t => t.Gender)
               .HasColumnName("Gender")
               .HasColumnType("varchar(10)")
               .IsUnicode(false)
               .HasMaxLength(10)
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(t => t.PhoneNumber)
               .HasColumnName("PhoneNumber")
               .HasColumnType("varchar(50)")
               .IsUnicode(false)
               .HasMaxLength(25)
               .ValueGeneratedNever()
               .IsRequired();
            
        builder.Property(t => t.DateAttended)
               .HasColumnName("DateAttended")
               .HasColumnType("datetime")
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(t => t.CreatedAt)
               .HasColumnName("CreatedAt")
               .HasColumnType("datetime")
               .IsRequired()
               .ValueGeneratedNever();

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
        builder.HasOne(m => m.Tenant)
               .WithMany()
               .HasForeignKey(m => m.TenantId)
               .HasConstraintName("FK_NewComers_TenantId_Tenants_TenantId")
               .OnDelete(DeleteBehavior.Restrict);
            
            
        // Indexes and Constraints
        // UQ

        // CK
        builder.HasCheckConstraint("CK_NewComers_TenantId_Name_Surname",
                                   "TenantId>(0) AND Name IS NOT NULL " +
                                   "AND [Surname] IS NOT NULL AND Gender IS NOT NULL");

    }

}