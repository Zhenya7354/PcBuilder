using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class PsuConfiguration : IEntityTypeConfiguration<PsuEntity>
{
    public void Configure(EntityTypeBuilder<PsuEntity> builder)
    {
        builder.ToTable("Psu")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.BrandId).HasColumnName("brand_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.Wattage).HasColumnName("wattage");
        builder.Property(e => e.Efficiency).HasColumnName("efficiency").HasColumnType("psu_rating");
        builder.Property(e => e.Modularity).HasColumnName("modular").HasColumnType("psu_modular");
        builder.Property(e => e.AtxVersion).HasColumnName("atx_version");
        builder.Property(e => e.Has16Pin).HasColumnName("has_16pin");
        builder.Property(e => e.EpsConnectors).HasColumnName("eps_connectors");
        builder.Property(e => e.SataConnectors).HasColumnName("sata_connectors");
        builder.Property(e => e.Pcie8PinConnectors).HasColumnName("pcie_8pin");
        builder.Property(e => e.FanSizeMm).HasColumnName("fan_size_mm");
        builder.Property(e => e.LengthMm).HasColumnName("length_mm");
        builder.Property(e => e.PriceUsd).HasColumnName("price_usd");

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
