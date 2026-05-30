using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Entities;

namespace PcBuilder.Data;

public class PcDbContext(DbContextOptions<PcDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<UserEntity> User { get; set; }
    public DbSet<BuildEntity> Build { get; set; }
    public DbSet<RegularUserEntity> RegularUser { get; set; }
    public DbSet<AdminEntity> Admin { get; set; }
    public DbSet<CpuEntity> Cpu { get; set; }
    public DbSet<CpuCoolerEntity> CpuCooler { get; set; }
    public DbSet<PcCaseEntity> PcCase { get; set; }
    public DbSet<GpuEntity> Gpu { get; set; }
    public DbSet<HardDriveEntity> HardDrive { get; set; }
    public DbSet<MotherboardEntity> Motherboard { get; set; }
    public DbSet<PsuEntity> Psu { get; set; } 
    public DbSet<RamEntity> Ram { get; set; }
    public DbSet<PcMonitorEntity> PcMonitor { get; set; }
    public DbSet<BrandEntity> Brand { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PcDbContext).Assembly);
    }
}
