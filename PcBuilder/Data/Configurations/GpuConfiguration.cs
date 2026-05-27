using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Configurations;

public class GpuConfiguration : IEntityTypeConfiguration<GpuEntity>
{
    public void Configure(EntityTypeBuilder<GpuEntity> builder)
    {
        builder.ToTable("Gpu")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
        builder
            .Property(g => g.GpuInterface)
            .HasConversion(
               v => v.ToString(),
                v => Enum.Parse<GpuInterface>(v));

    }
}
