using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class PcCaseConfiguration : IEntityTypeConfiguration<PcCaseEntity>
{
    public void Configure(EntityTypeBuilder<PcCaseEntity> builder)
    {
        builder.ToTable("PcCase")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.BrandId).HasColumnName("brand_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.SupportedFormFactors).HasColumnName("supported_form_factors");
        builder.Property(e => e.MaxGpuLengthMm).HasColumnName("max_gpu_length_mm");
        builder.Property(e => e.MaxCpuCoolerHeightMm).HasColumnName("max_cpu_cooler_height_mm");
        builder.Property(e => e.MaxPsuLengthMm).HasColumnName("max_psu_length_mm");
        builder.Property(e => e.DriveBays35Inch).HasColumnName("drive_bays_35");
        builder.Property(e => e.DriveBays25Inch).HasColumnName("drive_bays_25");
        builder.Property(e => e.FrontUsbA).HasColumnName("front_usb_a");
        builder.Property(e => e.FrontUsbC).HasColumnName("front_usb_c");
        builder.Property(e => e.RadiatorSupportMm).HasColumnName("radiator_support_mm");
        builder.Property(e => e.CaseWidthMm).HasColumnName("case_width_mm");
        builder.Property(e => e.CaseHeightMm).HasColumnName("case_height_mm");
        builder.Property(e => e.CaseDepthMm).HasColumnName("case_depth_mm");
        builder.Property(e => e.HasGlassPanel).HasColumnName("has_glass_panel");
        builder.Property(e => e.IncludedFans).HasColumnName("includes_fans");
        builder.Property(e => e.PriceUsd).HasColumnName("price_usd");

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
