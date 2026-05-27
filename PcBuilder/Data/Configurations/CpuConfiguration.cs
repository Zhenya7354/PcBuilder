using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class CpuConfiguration : IEntityTypeConfiguration<CpuEntity>
{
    public void Configure(EntityTypeBuilder<CpuEntity> builder)
    {
        builder.ToTable("Cpu")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
