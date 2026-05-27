using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PcBuilder.Entities;
using PcBuilder.Enums;

namespace PcBuilder.Data.Configurations;

public class PcCaseConfiguration : IEntityTypeConfiguration<PcCaseEntity>
{
    public void Configure(EntityTypeBuilder<PcCaseEntity> builder)
    {
        builder.ToTable("PcCase")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
        var valueComparer = new ValueComparer<List<FormFactor>>(
    (c1, c2) => c1!.SequenceEqual(c2!),
    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
    c => c.ToList());

        var valerConverter = new ValueConverter<List<FormFactor>, List<string>>(
            v => v.Select(v => v.ToString()).ToList(),
            v => v.Select(v => Enum.Parse<FormFactor>(v)).ToList());
        builder
            .Property(m => m.SupportedFormFactors)
            .HasConversion(valerConverter, valueComparer);
    }
}
