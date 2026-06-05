using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Configurations;

public class RamConfiguration : IEntityTypeConfiguration<RamEntity>
{
    public void Configure(EntityTypeBuilder<RamEntity> builder)
    {
        

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
        builder
            .Property(m => m.MemoryType)
            .HasConversion(
               v => v.ToString(),
                v => Enum.Parse<MemoryType>(v));
        builder
            .Property(e => e.Currency)
            .HasConversion<string>();
        builder
            .Property(e => e.ColorScheme)
            .HasConversion<string>();
    }
}
