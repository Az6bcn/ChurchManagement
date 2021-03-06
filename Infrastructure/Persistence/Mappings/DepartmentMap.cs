using Domain.Entities.PersonAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            
            builder.HasQueryFilter(d => d.Deleted == null);

            // PK
            builder.HasKey(x => x.DepartmentId)
                .HasName("PK_Departments_DepartmentId")
                .IsClustered();

            // Columns
            builder.Property(t => t.DepartmentId)
                .HasColumnName("DepartmentId")
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

            // Indexes and Contraints
            // UQ
            builder.HasIndex(uq => uq.Name) 
                .HasDatabaseName("UQ_DepartmentName");
        }
    }
}
