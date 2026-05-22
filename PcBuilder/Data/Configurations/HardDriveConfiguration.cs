using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class HardDriveConfiguration : IEntityTypeConfiguration<HardDriveEntity>
{
    public void Configure(EntityTypeBuilder<HardDriveEntity> builder)
    {
        builder.ToTable("HardDrive")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.BrandId).HasColumnName("brand_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.CapacityGb).HasColumnName("capacity_gb");
        builder.Property(e => e.DriveInterface).HasColumnName("interface").HasColumnType("storage_interface");
        builder.Property(e => e.FormFactor).HasColumnName("form_factor").HasColumnType("storage_form_factor");
        builder.Property(e => e.IsSsd).HasColumnName("is_ssd");
        builder.Property(e => e.ReadSpeedMbS).HasColumnName("read_speed_mbs");
        builder.Property(e => e.WriteSpeedMbs).HasColumnName("write_speed_mbs");
        builder.Property(e => e.Rpm).HasColumnName("rpm");
        builder.Property(e => e.CacheMb).HasColumnName("cache_mb");
        builder.Property(e => e.Tbw).HasColumnName("tbw");
        builder.Property(e => e.PowerWatts).HasColumnName("power_watts");
        builder.Property(e => e.PriceUsd).HasColumnName("price_usd");

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
