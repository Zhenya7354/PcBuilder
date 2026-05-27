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

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
