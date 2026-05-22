using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class RamConfiguration : IEntityTypeConfiguration<RamEntity>
{
    public void Configure(EntityTypeBuilder<RamEntity> builder)
    {
        builder.ToTable("Ram")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.BrandId).HasColumnName("brand_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.MemoryType).HasColumnName("memory_type").HasColumnType("memory_type");
        builder.Property(e => e.CapacityGb).HasColumnName("capacity_gb");
        builder.Property(e => e.KitCount).HasColumnName("kit_count");
        builder.Property(e => e.SpeedMhz).HasColumnName("speed_mhz");
        builder.Property(e => e.CasLatency).HasColumnName("cas_latency");
        builder.Property(e => e.Voltage).HasColumnName("voltage");
        builder.Property(e => e.HasRgb).HasColumnName("has_rgb");
        builder.Property(e => e.HasEcc).HasColumnName("has_ecc");
        builder.Property(e => e.HeightMm).HasColumnName("height_mm");
        builder.Property(e => e.PriceUsd).HasColumnName("price_usd");

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
