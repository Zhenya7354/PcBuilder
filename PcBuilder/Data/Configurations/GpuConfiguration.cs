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

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.BrandId).HasColumnName("brand_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.GpuChip).HasColumnName("gpu_chip");
        builder.Property(e => e.GpuInterface).HasColumnName("interface").HasColumnType("gpu_interface");
        builder.Property(e => e.VramGb).HasColumnName("vram_gb");
        builder.Property(e => e.VramType).HasColumnName("vram_type");
        builder.Property(e => e.BaseClockMhz).HasColumnName("base_clock_mhz");
        builder.Property(e => e.BoostClockMhz).HasColumnName("boost_clock_mhz");
        builder.Property(e => e.MemoryBusBits).HasColumnName("memory_bus_bits");
        builder.Property(e => e.MemoryBandwithGb).HasColumnName("memory_bandwidth_gbps");
        builder.Property(e => e.TdpWatts).HasColumnName("tdp_watts");
        builder.Property(e => e.RecommendedPsuWattage).HasColumnName("recommended_psu_w");
        builder.Property(e => e.PowerConnectors).HasColumnName("power_connectors");
        builder.Property(e => e.OutputHdmi).HasColumnName("outputs_hdmi");
        builder.Property(e => e.OutputDp).HasColumnName("outputs_dp");
        builder.Property(e => e.CardLengthMm).HasColumnName("card_length_mm");
        builder.Property(e => e.CardSlots).HasColumnName("card_slots");
        builder.Property(e => e.HasRgb).HasColumnName("has_rgb");
        builder.Property(e => e.Price).HasColumnName("price_usd");

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
