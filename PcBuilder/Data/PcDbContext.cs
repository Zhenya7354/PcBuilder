using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Data
{
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
                .HasOne(b => b.user)
                .WithMany()
                .HasForeignKey(b => b.userId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Build>()
                .HasOne(b => b.cpu)
                .WithMany()
                .HasForeignKey(b => b.cpuId)
                .IsRequired(false);

            
            modelBuilder.Entity<Build>()
                .HasOne(b => b.gpu)
                .WithMany()
                .HasForeignKey(b => b.gpuId)
                .IsRequired(false);

            
            modelBuilder.Entity<Build>()
                .HasOne(b => b.motherboard)
                .WithMany()
                .HasForeignKey(b => b.motherboardId)
                .IsRequired(false);

            
            modelBuilder.Entity<Build>()
                .HasOne(b => b.hardDrive)
                .WithMany()
                .HasForeignKey(b => b.hardDriveId)
                .IsRequired(false);

            
            modelBuilder.Entity<Build>()
                .HasOne(b => b.cpuCooler)
                .WithMany()
                .HasForeignKey(b => b.cpuCoolerId)
                .IsRequired(false);

            
            modelBuilder.Entity<Build>()
                .HasOne(b => b.monitor)
                .WithMany()
                .HasForeignKey(b => b.monitorId)
                .IsRequired(false);

            modelBuilder.Entity<Build>()
                .HasOne(b => b.ram)
                .WithMany()
                .HasForeignKey(b => b.ramId)
                .IsRequired(false);

            modelBuilder.Entity<Build>()
                .HasOne(b => b.psu)
                .WithMany()
                .HasForeignKey(b => b.psuId)
                .IsRequired(false);

            modelBuilder.Entity<Build>()
                .HasOne(b => b.pcCase)
                .WithMany()
                .HasForeignKey(b => b.caseId)
                .IsRequired(false);

            modelBuilder.Entity<Cpu>()
                .HasOne(c => c.brand)
                .WithMany()
                .HasForeignKey(c => c.brandId);

            modelBuilder.Entity<Gpu>()
                .HasOne(c => c.brand)
                .WithMany()
                .HasForeignKey(c => c.brandId);

            modelBuilder.Entity<Psu>()
                .HasOne(c => c.brand)
                .WithMany()
                .HasForeignKey(c => c.brandId);

            modelBuilder.Entity<PcMonitor>()
                .HasOne(c => c.brand)
                .WithMany()
                .HasForeignKey(c => c.brandId);

            modelBuilder.Entity<Ram>()
                .HasOne(c => c.brand)
                .WithMany()
                .HasForeignKey(c => c.brandId);

            modelBuilder.Entity<HardDrive>()
                .HasOne(c => c.brand)
                .WithMany()
                .HasForeignKey(c => c.brandId);

            modelBuilder.Entity<PcCase>()
                .HasOne(c => c.brand)
                .WithMany()
                .HasForeignKey(c => c.brandId);

            modelBuilder.Entity<Motherboard>()
                .HasOne(c => c.brand)
                .WithMany()
                .HasForeignKey(c => c.brandId);

            modelBuilder.Entity<CpuCooler>()
                .HasOne(c => c.brand)
                .WithMany()
                .HasForeignKey(c => c.brandId);




        }
        private static void ConfigurePostgresEnumColumns(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cpu>()
                .Property(c => c.socket)
                .HasColumnType("socket_type");
            modelBuilder.Entity<Cpu>()
                .Property(c => c.memoryType)
                .HasColumnType("memory_type");

            modelBuilder.Entity<CpuCooler>()
                .Property(c => c.coolerType)
                .HasColumnType("cooler_type");

            modelBuilder.Entity<Gpu>()
                .Property(g => g.gpuInterface)
                .HasColumnType("gpu_interface");

            modelBuilder.Entity<HardDrive>()
                .Property(h => h.driveInterface)
                .HasColumnType("storage_interface");
            modelBuilder.Entity<HardDrive>()
                .Property(h => h.formFactor)
                .HasColumnType("storage_form_factor");

            modelBuilder.Entity<Motherboard>()
                .Property(m => m.socket)
                .HasColumnType("socket_type");
            modelBuilder.Entity<Motherboard>()
                .Property(m => m.formFactor)
                .HasColumnType("form_factor");
            modelBuilder.Entity<Motherboard>()
                .Property(m => m.memoryType)
                .HasColumnType("memory_type");

            modelBuilder.Entity<PcMonitor>()
                .Property(m => m.panelType)
                .HasColumnType("panel_type");

            modelBuilder.Entity<Psu>()
                .Property(p => p.efficiency)
                .HasColumnType("psu_rating");
            modelBuilder.Entity<Psu>()
                .Property(p => p.modularity)
                .HasColumnType("psu_modular");

            modelBuilder.Entity<Ram>()
                .Property(r => r.memoryType)
                .HasColumnType("memory_type");
        }
    }
}
