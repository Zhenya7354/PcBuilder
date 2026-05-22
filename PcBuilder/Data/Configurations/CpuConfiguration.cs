using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class CpuConfiguration : IEntityTypeConfiguration<CpuEntity>
{
    public void Configure(EntityTypeBuilder<CpuEntity> builder)
    {
        builder.ToTable("Cpu")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.BrandId).HasColumnName("brand_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.ModelNumber).HasColumnName("model_number");
        builder.Property(e => e.Socket).HasColumnName("socket").HasColumnType("socket_type");
        builder.Property(e => e.ChipsetsSupported).HasColumnName("chipsets_supported");
        builder.Property(e => e.Cores).HasColumnName("cores");
        builder.Property(e => e.Threads).HasColumnName("threads");
        builder.Property(e => e.BaseClockGhz).HasColumnName("base_clock_ghz");
        builder.Property(e => e.BoostClockGhz).HasColumnName("boost_clock_ghz");
        builder.Property(e => e.L3CacheMb).HasColumnName("l3_cache_mb");
        builder.Property(e => e.TdpWatts).HasColumnName("tdp_watts");
        builder.Property(e => e.MemoryType).HasColumnName("memory_type").HasColumnType("memory_type");
        builder.Property(e => e.MaxMemoryGb).HasColumnName("max_memory_gb");
        builder.Property(e => e.MaxMemorySpeedMhz).HasColumnName("max_memory_speed_mhz");
        builder.Property(e => e.MemoryChannels).HasColumnName("memory_channels");
        builder.Property(e => e.IntegratedGraphics).HasColumnName("integrated_graphics");
        builder.Property(e => e.IgpuModel).HasColumnName("igpu_model");
        builder.Property(e => e.PcieVersion).HasColumnName("pcie_version");
        builder.Property(e => e.PcieLanes).HasColumnName("pcie_lanes");
        builder.Property(e => e.IncludesCooler).HasColumnName("includes_cooler");
        builder.Property(e => e.LaunchedYear).HasColumnName("launched_year");
        builder.Property(e => e.PriceUsd).HasColumnName("price_usd");

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
