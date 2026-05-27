using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

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
    }
}
