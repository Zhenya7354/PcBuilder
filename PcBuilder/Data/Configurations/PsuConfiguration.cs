using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class PsuConfiguration : IEntityTypeConfiguration<PsuEntity>
{
    public void Configure(EntityTypeBuilder<PsuEntity> builder)
    {
        builder.ToTable("Psu")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(e => e.BrandId);
    }
}
