using Domain.Entities.PersonAggregate;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mappings
{
    public class MinisterTitleMap : IEntityTypeConfiguration<MinisterTitle>
    {
        public void Configure(EntityTypeBuilder<MinisterTitle> builder)
        {
            builder.ToTable("MinisterTitle");

            // PK
            builder.HasKey(x => x.MinisterTitleId)
                .HasName("PK_ServiceTypes_MinisterTitleId")
                .IsClustered();

            // Columns
            builder.Property(t => t.MinisterTitleId)
                .HasColumnName("MinisterTitleId")
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

            // seed
            builder.HasData(
                MinisterTitle.Create(1, "Pastor"),
                MinisterTitle.Create(2, "Assistant Pastor"),
                MinisterTitle.Create(3, "Deacon"),
                MinisterTitle.Create(4, "Deaconess")
            );
        }
    }
}