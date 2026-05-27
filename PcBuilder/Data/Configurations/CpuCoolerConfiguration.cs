using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PcBuilder.Entities;
using PcBuilder.Enums;

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

        var valueComparer = new ValueComparer<List<PcSocketType>>(
    (c1, c2) => c1!.SequenceEqual(c2!),
    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
    c => c.ToList());

        var valerConverter = new ValueConverter<List<PcSocketType>, List<string>>(
            v => v.Select(v => v.ToString()).ToList(),
            v => v.Select(v => Enum.Parse<PcSocketType>(v)).ToList());
        builder
            .Property(e => e.SocketsSupported)
            .HasConversion(valerConverter, valueComparer);

        builder
            .Property(e => e.CoolerType)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<CoolerType>(v));
    }
}
