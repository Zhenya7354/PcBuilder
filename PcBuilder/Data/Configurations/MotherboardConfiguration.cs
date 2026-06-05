using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Configurations;

public class MotherboardConfiguration : IEntityTypeConfiguration<MotherboardEntity>
{
    public void Configure(EntityTypeBuilder<MotherboardEntity> builder)
    {
        

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
        builder
            .Property(e => e.Socket)
            .HasConversion(
               v => v.ToString(),
                v => Enum.Parse<PcSocketType>(v));
        builder
            .Property(m => m.FormFactor)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<FormFactor>(v));
        builder
            .Property(m => m.MemoryType)
            .HasConversion(
               v => v.ToString(),
                v => Enum.Parse<MemoryType>(v));
        builder
            .Property(e => e.Currency)
            .HasConversion<string>();
    }
}
