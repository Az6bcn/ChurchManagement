using Domain.Entities.AttendanceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings;

public class AttendanceMap : IEntityTypeConfiguration<Attendance>
{
       public void Configure(EntityTypeBuilder<Attendance> builder)
       {
              builder.ToTable("Attendances");

              builder.HasQueryFilter(m => m.Deleted == null);

              // PK
              builder.HasKey(m => m.AttendanceId)
                     .HasName("PK_Attendances_AttendanceId")
                     .IsClustered();

              // Columns
              builder.Property(m => m.AttendanceId)
                     .HasColumnName("AttendanceId")
                     //.HasColumnType("int")
                     .UseIdentityColumn()
                     .ValueGeneratedOnAdd()
                     .IsRequired();

              builder.Property(t => t.TenantId)
                     .HasColumnName("TenantId")
                     //.HasColumnType("int")
                     .ValueGeneratedNever()
                     .IsRequired();

              builder.Property(t => t.Male)
                     .HasColumnName("Male")
                     //.HasColumnType("int")
                     .ValueGeneratedNever()
                     .IsRequired();

              builder.Property(t => t.Female)
                     .HasColumnName("Female")
                     //.HasColumnType("int")
                     .ValueGeneratedNever()
                     .IsRequired();

              builder.Property(t => t.Children)
                     .HasColumnName("Children")
                     //.HasColumnType("int")
                     .ValueGeneratedNever()
                     .IsRequired();

              builder.Property(t => t.NewComers)
                     .HasColumnName("NewComers")
                     //.HasColumnType("int")
                     .ValueGeneratedNever()
                     .IsRequired();
            
              builder.Property(t => t.ServiceTypeId)
                     .HasColumnName("ServiceTypeId")
                     //.HasColumnType("int")
                     .ValueGeneratedNever()
                     .IsRequired();
            
              builder.Property(t => t.ServiceDate)
                     .HasColumnName("ServiceDate")
                     .HasColumnType("date")
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
              builder.HasOne(a => a.Tenant)
                     .WithMany()
                     .HasForeignKey(a => a.TenantId)
                     .HasConstraintName("Attendances_TenantId_Tenants_TenantId")
                     .OnDelete(DeleteBehavior.Restrict);

              // Indexes and Constraints
              // TODO : TenantId > 0
       }
}