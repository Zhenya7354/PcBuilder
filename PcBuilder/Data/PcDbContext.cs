using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Data;

public class PcDbContext(DbContextOptions<PcDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<User> user { get; set; }
    public DbSet<Build> Build { get; set; }
    public DbSet<RegularUser> RegularUser { get; set; }
    public DbSet<Admin> Admin { get; set; }
    public DbSet<Cpu> Cpu { get; set; }
    public DbSet<CpuCooler> CpuCooler { get; set; }
    public DbSet<PcCase> PcCase { get; set; }
    public DbSet<Gpu> Gpu { get; set; }
    public DbSet<HardDrive> HardDrive { get; set; }
    public DbSet<Motherboard> Motherboard { get; set; }
    public DbSet<Psu> Psu { get; set; } 
    public DbSet<Ram> Ram { get; set; }
    public DbSet<PcMonitor> PcMonitor { get; set; }
    public DbSet<Brand> Brand { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure relationships and constraints here if needed
        modelBuilder.Entity<Cpu>().ToTable("Cpu")
            .Metadata.SetIsTableExcludedFromMigrations(true); 
        modelBuilder.Entity<CpuCooler>().ToTable("CpuCooler")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<PcCase>().ToTable("PcCase")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<Motherboard>().ToTable("Motherboard")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<HardDrive>().ToTable("HardDrive")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<Gpu>().ToTable("Gpu")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<Psu>().ToTable("Psu")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<Ram>().ToTable("Ram")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<PcMonitor>().ToTable("PcMonitor")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<Brand>().ToTable("Brand")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        ConfigurePostgresEnumColumns(modelBuilder);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.Cpu)
            .WithMany()
            .HasForeignKey(b => b.CpuId)
            .IsRequired(false);

        
        modelBuilder.Entity<Build>()
            .HasOne(b => b.Gpu)
            .WithMany()
            .HasForeignKey(b => b.GpuId)
            .IsRequired(false);

        
        modelBuilder.Entity<Build>()
            .HasOne(b => b.Motherboard)
            .WithMany()
            .HasForeignKey(b => b.MotherboardId)
            .IsRequired(false);

        
        modelBuilder.Entity<Build>()
            .HasOne(b => b.HardDrive)
            .WithMany()
            .HasForeignKey(b => b.HardDriveId)
            .IsRequired(false);

        
        modelBuilder.Entity<Build>()
            .HasOne(b => b.CpuCooler)
            .WithMany()
            .HasForeignKey(b => b.CpuCoolerId)
            .IsRequired(false);

        
        modelBuilder.Entity<Build>()
            .HasOne(b => b.Monitor)
            .WithMany()
            .HasForeignKey(b => b.MonitorId)
            .IsRequired(false);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.Ram)
            .WithMany()
            .HasForeignKey(b => b.RamId)
            .IsRequired(false);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.Psu)
            .WithMany()
            .HasForeignKey(b => b.PsuId)
            .IsRequired(false);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.PcCase)
            .WithMany()
            .HasForeignKey(b => b.CaseId)
            .IsRequired(false);

        modelBuilder.Entity<Cpu>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<Gpu>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<Psu>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<PcMonitor>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<Ram>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<HardDrive>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<PcCase>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<Motherboard>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<CpuCooler>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);




    }
    private static void ConfigurePostgresEnumColumns(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cpu>()
            .Property(c => c.Socket)
            .HasColumnType("socket_type");
        modelBuilder.Entity<Cpu>()
            .Property(c => c.MemoryType)
            .HasColumnType("memory_type");

        modelBuilder.Entity<CpuCooler>()
            .Property(c => c.CoolerType)
            .HasColumnType("cooler_type");

        modelBuilder.Entity<Gpu>()
            .Property(g => g.GpuInterface)
            .HasColumnType("gpu_interface");

        modelBuilder.Entity<HardDrive>()
            .Property(h => h.DriveInterface)
            .HasColumnType("storage_interface");
        modelBuilder.Entity<HardDrive>()
            .Property(h => h.FormFactor)
            .HasColumnType("storage_form_factor");

        modelBuilder.Entity<Motherboard>()
            .Property(m => m.Socket)
            .HasColumnType("socket_type");
        modelBuilder.Entity<Motherboard>()
            .Property(m => m.FormFactor)
            .HasColumnType("form_factor");
        modelBuilder.Entity<Motherboard>()
            .Property(m => m.MemoryType)
            .HasColumnType("memory_type");

        modelBuilder.Entity<PcMonitor>()
            .Property(m => m.PanelType)
            .HasColumnType("panel_type");

        modelBuilder.Entity<Psu>()
            .Property(p => p.Efficiency)
            .HasColumnType("psu_rating");
        modelBuilder.Entity<Psu>()
            .Property(p => p.Modularity)
            .HasColumnType("psu_modular");

        modelBuilder.Entity<Ram>()
            .Property(r => r.MemoryType)
            .HasColumnType("memory_type");
    }
}
