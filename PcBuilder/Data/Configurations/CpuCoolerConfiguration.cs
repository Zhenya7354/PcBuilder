using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class CpuCoolerConfiguration : IEntityTypeConfiguration<CpuCoolerEntity>
{
    public void Configure(EntityTypeBuilder<CpuCoolerEntity> builder)
    {
        builder.ToTable("CpuCooler")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.BrandId).HasColumnName("brand_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.CoolerType).HasColumnName("cooler_type").HasColumnType("cooler_type");
        builder.Property(e => e.SocketsSupported).HasColumnName("socket_support");
        builder.Property(e => e.RadiatorSizeMm).HasColumnName("radiator_size_mm");
        builder.Property(e => e.FanCount).HasColumnName("fan_count");
        builder.Property(e => e.FanSizeMm).HasColumnName("fan_size_mm");
        builder.Property(e => e.MaxTdpWatts).HasColumnName("max_tdp_watts");
        builder.Property(e => e.HeightMm).HasColumnName("height_mm");
        builder.Property(e => e.HasRgb).HasColumnName("has_rgb");
        builder.Property(e => e.NoiseLevelDb).HasColumnName("noise_db");
        builder.Property(e => e.PriceUsd).HasColumnName("price_usd");

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
