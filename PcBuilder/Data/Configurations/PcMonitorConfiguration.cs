using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class PcMonitorConfiguration : IEntityTypeConfiguration<PcMonitorEntity>
{
    public void Configure(EntityTypeBuilder<PcMonitorEntity> builder)
    {
        builder.ToTable("PcMonitor")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.BrandId).HasColumnName("brand_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.ScreenSizeInch).HasColumnName("screen_size_inch");
        builder.Property(e => e.ResolutionWidth).HasColumnName("resolution_w");
        builder.Property(e => e.ResolutionHeight).HasColumnName("resolution_h");
        builder.Property(e => e.PanelType).HasColumnName("panel_type").HasColumnType("panel_type");
        builder.Property(e => e.RefreshRateHz).HasColumnName("refresh_rate_hz");
        builder.Property(e => e.ResponseTimeMs).HasColumnName("response_time_ms");
        builder.Property(e => e.HdrSupport).HasColumnName("hdr_support");
        builder.Property(e => e.BrightnessNits).HasColumnName("brightness_nits");
        builder.Property(e => e.ContrastRatio).HasColumnName("contrast_ratio");
        builder.Property(e => e.ColorGamutP3).HasColumnName("color_gamut_p3");
        builder.Property(e => e.HasGSync).HasColumnName("has_gsync");
        builder.Property(e => e.HasFreeSync).HasColumnName("has_freesync");
        builder.Property(e => e.HasFreeSyncPremium).HasColumnName("has_freesync_premium");
        builder.Property(e => e.HdmiPorts).HasColumnName("inputs_hdmi");
        builder.Property(e => e.HdmiVersion).HasColumnName("hdmi_version");
        builder.Property(e => e.DpPorts).HasColumnName("inputs_dp");
        builder.Property(e => e.DpVersion).HasColumnName("dp_version");
        builder.Property(e => e.UsbCPorts).HasColumnName("inputs_usb_c");
        builder.Property(e => e.HasUsbHub).HasColumnName("has_usb_hub");
        builder.Property(e => e.HasSpeakers).HasColumnName("has_speakers");
        builder.Property(e => e.HeightAdjustable).HasColumnName("height_adjustable");
        builder.Property(e => e.VesaMount).HasColumnName("vesa_mount");
        builder.Property(e => e.PriceUsd).HasColumnName("price_usd");

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
