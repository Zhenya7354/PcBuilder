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
        

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
        
        builder
            .PrimitiveCollection(e => e.SupportedFormFactors);
    }
}
