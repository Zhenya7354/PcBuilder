using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;
using PcBuilder.Enums;

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

        builder
            .Property(e => e.Socket)
            .HasConversion(
               v => v.ToString(),
                v => Enum.Parse<PcSocketType>(v));
        builder
            .Property(e => e.MemoryType)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<MemoryType>(v));
    }
}
