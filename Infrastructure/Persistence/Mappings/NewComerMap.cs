using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class NewComerMap: IEntityTypeConfiguration<NewComer>
    {
        public void Configure(EntityTypeBuilder<NewComer> builder)
        {
            builder.ToTable("NewComers");

            // Can only be applied to root entity Member(base)
            //builder.HasQueryFilter(m => m.Deleted == null);

            // PK
            // Cannot re-assign PK because already been done in Member(base)
            // builder.HasKey(m => m.NewComerId)
            //     .HasName("PK_Ministers_MinisterId")
            //     .IsClustered();

            // Columns
            builder.Property(m => m.NewComerId)
                .HasColumnName("NewComerId")
                .HasColumnType("int")
                .UseIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(t => t.TenantId)
                .HasColumnName("TenantId")
                .HasColumnType("int")
                .ValueGeneratedNever()
                .IsRequired();
            
            builder.Property(t => t.ServiceTypeId)
                .HasColumnName("ServiceTypeId")
                .HasColumnType("int")
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

            builder.Property(t => t.DateAndMonthOfBirth)
                .HasColumnName("DateAndMonthOfBirth")
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
                .HasColumnType("varchar(25)")
                .IsUnicode(false)
                .HasMaxLength(25)
                .ValueGeneratedNever()
                .IsRequired();

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
            builder.HasOne(m => m.ServiceType)
                .WithMany()
                .HasForeignKey(m => m.ServiceTypeId)
                .HasConstraintName("FK_NewComers_ServiceTypes_ServiceTypeIdId")
                .OnDelete(DeleteBehavior.Restrict);
            
            //  Navigation properties can only participate in a single relationship. A relationship already exists between 'Tenant.Members(base)'
            // builder.HasOne(t => t .Tenant)
            //     .WithMany(t => t.NewComers)
            //     .HasForeignKey(m => m.TenantId)
            //     .HasConstraintName("FK_Tenants_NewComers_TenantId")
            //     .OnDelete(DeleteBehavior.Restrict);
            
            // Indexes and Constraints
            // UQ
            builder.HasIndex(uq => new {uq.TenantId, uq.Name, uq.Surname, uq.DateAndMonthOfBirth })
                .HasDatabaseName("UQ_TenantId_Name_Surname_DateAndMonthOfBirth");
            
        }

    }
}
