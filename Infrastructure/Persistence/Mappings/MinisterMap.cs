using Domain.Entities.PersonAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class MinisterMap: IEntityTypeConfiguration<Minister>
    {
        public void Configure(EntityTypeBuilder<Minister> builder)
        {
            builder.ToTable("Ministers");

            // Can only be applied to root entity Member(base)
            builder.HasQueryFilter(m => m.Deleted == null);
            
            // PK
            //Cannot re-assign PK because already been done in Member(base)
            builder.HasKey(m => m.MinisterId)
                .HasName("PK_Ministers_MemberId")
                .IsClustered();

            // Columns
            builder.Property(m => m.MinisterId)
                .HasColumnName("MinisterId")
                .HasColumnType("int")
                .UseIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            builder.Property(t => t.TenantId)
                .HasColumnName("TenantId")
                .HasColumnType("int")
                .ValueGeneratedNever()
                .IsRequired();
            
            builder.Property(t => t.MinisterTitleId)
                .HasColumnName("MinisterTitleId")
                .HasColumnType("int")
                .ValueGeneratedNever()
                .IsRequired();
            
            builder.Property(t => t.MemberId)
                .HasColumnName("MemberId")
                .HasColumnType("int")
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
            // builder.HasOne(m => m.MinisterTitle)
            //     .WithMany(mt => mt.Ministers)
            //     .HasForeignKey(m => m.MinisterTitleId)
            //     .HasConstraintName("FK_Ministers_MinisterTitles_MinisterTitleId")
            //     .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Tenant)
                   .WithMany()
                   .HasForeignKey(m => m.TenantId)
                   .HasConstraintName("FK_Ministers_TenantId_Tenants_TenantId");
            //
            builder.HasOne(t => t.Member)
                .WithOne(m => m.Minister)
                .HasForeignKey<Minister>(m => m.MinisterId)
                .HasConstraintName("FK_Ministers_MinisterId_Members_MemberId")
                .OnDelete(DeleteBehavior.Restrict);
            
            
            // Indexes and Constraints
            // UQ
            builder.HasCheckConstraint("CK_Ministers_TenantId", "TenantId > 0");
            
            
            
            // Ignores
            builder.Ignore(m => m.Title);
        }

    }
}
