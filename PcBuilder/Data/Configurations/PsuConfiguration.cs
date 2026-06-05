using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Configurations;

public class PsuConfiguration : IEntityTypeConfiguration<PsuEntity>
{
    public void Configure(EntityTypeBuilder<PsuEntity> builder)
    {
        

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
        builder
            .Property(p => p.Efficiency)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<PsuRating>(v));
        builder
            .Property(p => p.Modularity)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<PsuModular>(v));
        builder
            .Property(e => e.Currency)
            .HasConversion<string>();

    }
}
