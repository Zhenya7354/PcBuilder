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

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.BrandId).HasColumnName("brand_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.Socket).HasColumnName("socket").HasColumnType("socket_type");
        builder.Property(e => e.Chipset).HasColumnName("chipset");
        builder.Property(e => e.FormFactor).HasColumnName("form_factor").HasColumnType("form_factor");
        builder.Property(e => e.MemoryType).HasColumnName("memory_type").HasColumnType("memory_type");
        builder.Property(e => e.MemorySlots).HasColumnName("memory_slots");
        builder.Property(e => e.MaxMemoryGb).HasColumnName("max_memory_gb");
        builder.Property(e => e.MaxMemorySpeedMhz).HasColumnName("max_memory_speed_mhz");
        builder.Property(e => e.PcieX16Slots).HasColumnName("pcie_x16_slots");
        builder.Property(e => e.PcieX1Slots).HasColumnName("pcie_x1_slots");
        builder.Property(e => e.M2Slots).HasColumnName("m2_slots");
        builder.Property(e => e.SataPorts).HasColumnName("sata_ports");
        builder.Property(e => e.UsbHeaders3Gen2).HasColumnName("usb_headers_3_2");
        builder.Property(e => e.UsbHeaders2Gen0).HasColumnName("usb_headers_2_0");
        builder.Property(e => e.HasWifi).HasColumnName("has_wifi");
        builder.Property(e => e.HasBluetooth).HasColumnName("has_bluetooth");
        builder.Property(e => e.LanSpeedGbps).HasColumnName("lan_speed_gbps");
        builder.Property(e => e.FanHeaders).HasColumnName("fan_headers");
        builder.Property(e => e.ArgbHeaders).HasColumnName("argb_headers");
        builder.Property(e => e.VrmPhases).HasColumnName("vrm_phases");
        builder.Property(e => e.RearUsbA).HasColumnName("rear_usb_a");
        builder.Property(e => e.RearUsbC).HasColumnName("rear_usb_c");
        builder.Property(e => e.RearHdmi).HasColumnName("rear_hdmi");
        builder.Property(e => e.RearDisplayPort).HasColumnName("rear_displayport");
        builder.Property(e => e.PriceUsd).HasColumnName("price_usd");

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
