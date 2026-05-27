using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class RamConfiguration : IEntityTypeConfiguration<RamEntity>
{
    public void Configure(EntityTypeBuilder<RamEntity> builder)
    {
        builder.ToTable("Ram")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
