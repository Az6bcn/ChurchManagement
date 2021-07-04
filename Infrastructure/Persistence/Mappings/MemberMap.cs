using Domain.Entities.PersonAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class MemberMap : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members");

            builder.HasQueryFilter(m => m.Deleted == null);
            builder.HasQueryFilter(d => d.Deleted == null);
            
            // PK
            builder.HasKey(m => m.MemberId)
                .HasName("PK_Members_MemberId")
                .IsClustered();

            // Columns
            builder.Property(m => m.MemberId)
                .HasColumnName("MemberId")
                //.HasColumnType("int")
                .UseIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(t => t.TenantId)
                .HasColumnName("TenantId")
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

            builder.Property(t => t.IsWorker)
             .HasColumnName("IsWorker")
             .HasColumnType("bit")
             .HasDefaultValueSql("0")
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
            builder.HasOne(x => x.Tenant)
                   .WithOne()
                   .HasForeignKey<Member>(x => x.TenantId)
                   .HasConstraintName("FK_Departments_TenantId_Tenants_TenantId");
            
            builder.HasMany(m => m.Departments)
                .WithMany(d => d.Members)
                .UsingEntity<DepartmentMembers>(
                jt => jt.HasOne(dm => dm.Department)
                    .WithMany(x => x.DepartmentMembers)
                    .HasForeignKey(dm => dm.DepartmentId)
                    .HasConstraintName("FK_DepartmentMembers_DepartmentMemberId_Departments_DepartmentId")
                    .OnDelete(DeleteBehavior.Restrict),
                jt => jt.HasOne(dm => dm.Member)
                    .WithMany(x => x.DepartmentMembers)
                    .HasForeignKey(dm => dm.MemberId)
                    .HasConstraintName("FK_DepartmentMembers_DepartmentMemberId_Members_DepartmentId")
                    .OnDelete(DeleteBehavior.Restrict),
                jt =>
                {
                    jt.HasKey(pk => new { pk.DepartmentId, pk.MemberId });
                });

            // Indexes and Constraints
            // UQ
            // TODO
            // builder.HasIndex(uq => new {uq.Person.TenantId, uq.Person.Name, uq.Person.Surname, uq.Person.DateAndMonthOfBirth })
            //     .HasDatabaseName("UQ_TenantId_Name_Surname_DateAndMonthOfBirth");
            
            // CK
            builder.HasCheckConstraint("CK_Members_TenantId_Name_Surname_DateMonthOfBirth", "[TenantId]>(0) " +
                                       "AND [Name] IS NOT NULL AND [Surname] IS NOT NULL AND [DateMonthOfBirth] IS NOT NULL");
        }
    }
}
