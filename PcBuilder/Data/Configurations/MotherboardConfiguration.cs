using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class MotherboardConfiguration : IEntityTypeConfiguration<MotherboardEntity>
{
    public void Configure(EntityTypeBuilder<MotherboardEntity> builder)
    {
        builder.ToTable("Motherboard")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
