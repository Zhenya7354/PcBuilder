using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Configurations;

public class HardDriveConfiguration : IEntityTypeConfiguration<HardDriveEntity>
{
    public void Configure(EntityTypeBuilder<HardDriveEntity> builder)
    {
        

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
        builder
            .Property(h => h.DriveInterface)
            .HasConversion(
            v => v.ToString(),
            v => Enum.Parse<StorageInterface>(v));
        builder
            .Property(h => h.FormFactor)
            .HasConversion(
            v => v.ToString(),
            v => Enum.Parse<StorageFormFactor>(v));
        builder
            .Property(e => e.Currency)
            .HasConversion<string>();
    }
}
