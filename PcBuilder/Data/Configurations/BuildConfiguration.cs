using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;

namespace PcBuilder.Data.Configurations;

public class BuildConfiguration : IEntityTypeConfiguration<BuildEntity>
{
    public void Configure(EntityTypeBuilder<BuildEntity> builder)
    {
        builder.HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Cpu)
            .WithMany()
            .HasForeignKey(b => b.CpuId)
            .IsRequired(false);

        builder.HasOne(b => b.Gpu)
            .WithMany()
            .HasForeignKey(b => b.GpuId)
            .IsRequired(false);

        builder.HasOne(b => b.Motherboard)
            .WithMany()
            .HasForeignKey(b => b.MotherboardId)
            .IsRequired(false);

        builder.HasOne(b => b.HardDrive)
            .WithMany()
            .HasForeignKey(b => b.HardDriveId)
            .IsRequired(false);

        builder.HasOne(b => b.CpuCooler)
            .WithMany()
            .HasForeignKey(b => b.CpuCoolerId)
            .IsRequired(false);

        builder.HasOne(b => b.Monitor)
            .WithMany()
            .HasForeignKey(b => b.MonitorId)
            .IsRequired(false);

        builder.HasOne(b => b.Ram)
            .WithMany()
            .HasForeignKey(b => b.RamId)
            .IsRequired(false);

        builder.HasOne(b => b.Psu)
            .WithMany()
            .HasForeignKey(b => b.PsuId)
            .IsRequired(false);

        builder.HasOne(b => b.PcCase)
            .WithMany()
            .HasForeignKey(b => b.CaseId)
            .IsRequired(false);
    }
}
