using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Configurations;

public class PcMonitorConfiguration : IEntityTypeConfiguration<PcMonitorEntity>
{
    public void Configure(EntityTypeBuilder<PcMonitorEntity> builder)
    {
        builder.ToTable("PcMonitor")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
        builder
            .Property(m => m.PanelType)
            .HasConversion(
            v => v.ToString(),
            v => Enum.Parse<PanelType>(v));
    }
}
