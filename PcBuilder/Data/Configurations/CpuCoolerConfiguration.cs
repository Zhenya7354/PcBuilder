using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PcBuilder.Entities;
using PcBuilder.Enums;
using System.Reflection.Emit;
using System.Text.Json;
using System.Xml;

namespace PcBuilder.Data.Configurations;

public class CpuCoolerConfiguration : IEntityTypeConfiguration<CpuCoolerEntity>
{
    public void Configure(EntityTypeBuilder<CpuCoolerEntity> builder)
    {
       

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);


        builder
        .PrimitiveCollection(e => e.SocketsSupported);


        builder
            .Property(e => e.CoolerType)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<CoolerType>(v));
    }
}
