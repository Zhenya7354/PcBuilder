using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Models;

namespace PcBuilder.Data
{
    public class PcDbContext(DbContextOptions<PcDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<User> user { get; set; }
        public DbSet<Build> build { get; set; }
        public DbSet<RegularUser> regular_user { get; set; }
        public DbSet<Admin> admin { get; set; }
        public DbSet<Cpu> cpu { get; set; }
        public DbSet<CpuCooler> cpu_cooler { get; set; }
        public DbSet<PcCase> pc_case { get; set; }
        public DbSet<Gpu> gpu { get; set; }
        public DbSet<HardDrive> hard_drive { get; set; }
        public DbSet<Motherboard> motherboard { get; set; }
        public DbSet<Psu> psu { get; set; } 
        public DbSet<Ram> memory { get; set; }
        public DbSet<PcMonitor> monitor { get; set; }
        public DbSet<Brand> brand { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure relationships and constraints here if needed
            modelBuilder.Entity<Cpu>().ToTable("cpu")
                .Metadata.SetIsTableExcludedFromMigrations(true); 
            modelBuilder.Entity<CpuCooler>().ToTable("cpu_cooler")
                .Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<PcCase>().ToTable("pc_case")
                .Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Motherboard>().ToTable("motherboard")
                .Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<HardDrive>().ToTable("hard_drive")
                .Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Gpu>().ToTable("gpu")
                .Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Psu>().ToTable("psu")
                .Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Ram>().ToTable("memory")
                .Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<PcMonitor>().ToTable("monitor")
                .Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Brand>().ToTable("brand")
                .Metadata.SetIsTableExcludedFromMigrations(true);

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

    }
}
