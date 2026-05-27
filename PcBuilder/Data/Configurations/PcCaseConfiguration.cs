using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

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
    }
}
