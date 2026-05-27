using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class CpuCoolerConfiguration : IEntityTypeConfiguration<CpuCoolerEntity>
{
    public void Configure(EntityTypeBuilder<CpuCoolerEntity> builder)
    {
        builder.ToTable("CpuCooler")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
