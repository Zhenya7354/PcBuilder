using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Entities;
using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Data;

public class PcDbContext(DbContextOptions<PcDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<UserEntity> user { get; set; }
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
        // Configure relationships and constraints here if needed
        modelBuilder.Entity<CpuEntity>().ToTable("Cpu")
            .Metadata.SetIsTableExcludedFromMigrations(true); 
        modelBuilder.Entity<CpuCoolerEntity>().ToTable("CpuCooler")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<PcCaseEntity>().ToTable("PcCase")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<MotherboardEntity>().ToTable("Motherboard")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<HardDriveEntity>().ToTable("HardDrive")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<GpuEntity>().ToTable("Gpu")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<PsuEntity>().ToTable("Psu")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<RamEntity>().ToTable("Ram")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<PcMonitorEntity>().ToTable("PcMonitor")
            .Metadata.SetIsTableExcludedFromMigrations(true);
        modelBuilder.Entity<BrandEntity>().ToTable("Brand")
            .Metadata.SetIsTableExcludedFromMigrations(true);

        ConfigureColumnNames(modelBuilder);
        ConfigureTablesForeignKeys(modelBuilder);
        ConfigurePostgresEnumColumns(modelBuilder);

    }

    private static void ConfigureColumnNames(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BrandEntity>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<CpuEntity>(entity =>
        {
            ConfigureComponentColumnNames(entity);
            entity.Property(e => e.ModelNumber).HasColumnName("model_number");
            entity.Property(e => e.Socket).HasColumnName("socket");
            entity.Property(e => e.ChipsetsSupported).HasColumnName("chipsets_supported");
            entity.Property(e => e.Cores).HasColumnName("cores");
            entity.Property(e => e.Threads).HasColumnName("threads");
            entity.Property(e => e.BaseClockGhz).HasColumnName("base_clock_ghz");
            entity.Property(e => e.BoostClockGhz).HasColumnName("boost_clock_ghz");
            entity.Property(e => e.L3CacheMb).HasColumnName("l3_cache_mb");
            entity.Property(e => e.TdpWatts).HasColumnName("tdp_watts");
            entity.Property(e => e.MemoryType).HasColumnName("memory_type");
            entity.Property(e => e.MaxMemoryGb).HasColumnName("max_memory_gb");
            entity.Property(e => e.MaxMemorySpeedMhz).HasColumnName("max_memory_speed_mhz");
            entity.Property(e => e.MemoryChannels).HasColumnName("memory_channels");
            entity.Property(e => e.IntegratedGraphics).HasColumnName("integrated_graphics");
            entity.Property(e => e.IgpuModel).HasColumnName("igpu_model");
            entity.Property(e => e.PcieVersion).HasColumnName("pcie_version");
            entity.Property(e => e.PcieLanes).HasColumnName("pcie_lanes");
            entity.Property(e => e.IncludesCooler).HasColumnName("includes_cooler");
            entity.Property(e => e.LaunchedYear).HasColumnName("launched_year");
            entity.Property(e => e.PriceUsd).HasColumnName("price_usd");
        });

        modelBuilder.Entity<CpuCoolerEntity>(entity =>
        {
            ConfigureComponentColumnNames(entity);
            entity.Property(e => e.CoolerType).HasColumnName("cooler_type");
            entity.Property(e => e.SocketsSupported).HasColumnName("socket_support");
            entity.Property(e => e.RadiatorSizeMm).HasColumnName("radiator_size_mm");
            entity.Property(e => e.FanCount).HasColumnName("fan_count");
            entity.Property(e => e.FanSizeMm).HasColumnName("fan_size_mm");
            entity.Property(e => e.MaxTdpWatts).HasColumnName("max_tdp_watts");
            entity.Property(e => e.HeightMm).HasColumnName("height_mm");
            entity.Property(e => e.HasRgb).HasColumnName("has_rgb");
            entity.Property(e => e.NoiseLevelDb).HasColumnName("noise_db");
            entity.Property(e => e.PriceUsd).HasColumnName("price_usd");
        });

        modelBuilder.Entity<GpuEntity>(entity =>
        {
            ConfigureComponentColumnNames(entity);
            entity.Property(e => e.GpuChip).HasColumnName("gpu_chip");
            entity.Property(e => e.GpuInterface).HasColumnName("interface");
            entity.Property(e => e.VramGb).HasColumnName("vram_gb");
            entity.Property(e => e.VramType).HasColumnName("vram_type");
            entity.Property(e => e.BaseClockMhz).HasColumnName("base_clock_mhz");
            entity.Property(e => e.BoostClockMhz).HasColumnName("boost_clock_mhz");
            entity.Property(e => e.MemoryBusBits).HasColumnName("memory_bus_bits");
            entity.Property(e => e.MemoryBandwithGb).HasColumnName("memory_bandwidth_gbps");
            entity.Property(e => e.TdpWatts).HasColumnName("tdp_watts");
            entity.Property(e => e.RecommendedPsuWattage).HasColumnName("recommended_psu_w");
            entity.Property(e => e.PowerConnectors).HasColumnName("power_connectors");
            entity.Property(e => e.OutputHdmi).HasColumnName("outputs_hdmi");
            entity.Property(e => e.OutputDp).HasColumnName("outputs_dp");
            entity.Property(e => e.CardLengthMm).HasColumnName("card_length_mm");
            entity.Property(e => e.CardSlots).HasColumnName("card_slots");
            entity.Property(e => e.HasRgb).HasColumnName("has_rgb");
            entity.Property(e => e.Price).HasColumnName("price_usd");
        });

        modelBuilder.Entity<HardDriveEntity>(entity =>
        {
            ConfigureComponentColumnNames(entity);
            entity.Property(e => e.CapacityGb).HasColumnName("capacity_gb");
            entity.Property(e => e.DriveInterface).HasColumnName("interface");
            entity.Property(e => e.FormFactor).HasColumnName("form_factor");
            entity.Property(e => e.IsSsd).HasColumnName("is_ssd");
            entity.Property(e => e.ReadSpeedMbS).HasColumnName("read_speed_mbs");
            entity.Property(e => e.WriteSpeedMbs).HasColumnName("write_speed_mbs");
            entity.Property(e => e.Rpm).HasColumnName("rpm");
            entity.Property(e => e.CacheMb).HasColumnName("cache_mb");
            entity.Property(e => e.Tbw).HasColumnName("tbw");
            entity.Property(e => e.PowerWatts).HasColumnName("power_watts");
            entity.Property(e => e.PriceUsd).HasColumnName("price_usd");
        });

        modelBuilder.Entity<MotherboardEntity>(entity =>
        {
            ConfigureComponentColumnNames(entity);
            entity.Property(e => e.Socket).HasColumnName("socket");
            entity.Property(e => e.Chipset).HasColumnName("chipset");
            entity.Property(e => e.FormFactor).HasColumnName("form_factor");
            entity.Property(e => e.MemoryType).HasColumnName("memory_type");
            entity.Property(e => e.MemorySlots).HasColumnName("memory_slots");
            entity.Property(e => e.MaxMemoryGb).HasColumnName("max_memory_gb");
            entity.Property(e => e.MaxMemorySpeedMhz).HasColumnName("max_memory_speed_mhz");
            entity.Property(e => e.PcieX16Slots).HasColumnName("pcie_x16_slots");
            entity.Property(e => e.PcieX1Slots).HasColumnName("pcie_x1_slots");
            entity.Property(e => e.M2Slots).HasColumnName("m2_slots");
            entity.Property(e => e.SataPorts).HasColumnName("sata_ports");
            entity.Property(e => e.UsbHeaders3Gen2).HasColumnName("usb_headers_3_2");
            entity.Property(e => e.UsbHeaders2Gen0).HasColumnName("usb_headers_2_0");
            entity.Property(e => e.HasWifi).HasColumnName("has_wifi");
            entity.Property(e => e.HasBluetooth).HasColumnName("has_bluetooth");
            entity.Property(e => e.LanSpeedGbps).HasColumnName("lan_speed_gbps");
            entity.Property(e => e.FanHeaders).HasColumnName("fan_headers");
            entity.Property(e => e.ArgbHeaders).HasColumnName("argb_headers");
            entity.Property(e => e.VrmPhases).HasColumnName("vrm_phases");
            entity.Property(e => e.RearUsbA).HasColumnName("rear_usb_a");
            entity.Property(e => e.RearUsbC).HasColumnName("rear_usb_c");
            entity.Property(e => e.RearHdmi).HasColumnName("rear_hdmi");
            entity.Property(e => e.RearDisplayPort).HasColumnName("rear_displayport");
            entity.Property(e => e.PriceUsd).HasColumnName("price_usd");
        });

        modelBuilder.Entity<PcCaseEntity>(entity =>
        {
            ConfigureComponentColumnNames(entity);
            entity.Property(e => e.SupportedFormFactors).HasColumnName("supported_form_factors");
            entity.Property(e => e.MaxGpuLengthMm).HasColumnName("max_gpu_length_mm");
            entity.Property(e => e.MaxCpuCoolerHeightMm).HasColumnName("max_cpu_cooler_height_mm");
            entity.Property(e => e.MaxPsuLengthMm).HasColumnName("max_psu_length_mm");
            entity.Property(e => e.DriveBays35Inch).HasColumnName("drive_bays_35");
            entity.Property(e => e.DriveBays25Inch).HasColumnName("drive_bays_25");
            entity.Property(e => e.FrontUsbA).HasColumnName("front_usb_a");
            entity.Property(e => e.FrontUsbC).HasColumnName("front_usb_c");
            entity.Property(e => e.RadiatorSupportMm).HasColumnName("radiator_support_mm");
            entity.Property(e => e.CaseWidthMm).HasColumnName("case_width_mm");
            entity.Property(e => e.CaseHeightMm).HasColumnName("case_height_mm");
            entity.Property(e => e.CaseDepthMm).HasColumnName("case_depth_mm");
            entity.Property(e => e.HasGlassPanel).HasColumnName("has_glass_panel");
            entity.Property(e => e.IncludedFans).HasColumnName("includes_fans");
            entity.Property(e => e.PriceUsd).HasColumnName("price_usd");
        });

        modelBuilder.Entity<PcMonitorEntity>(entity =>
        {
            ConfigureComponentColumnNames(entity);
            entity.Property(e => e.ScreenSizeInch).HasColumnName("screen_size_inch");
            entity.Property(e => e.ResolutionWidth).HasColumnName("resolution_w");
            entity.Property(e => e.ResolutionHeight).HasColumnName("resolution_h");
            entity.Property(e => e.PanelType).HasColumnName("panel_type");
            entity.Property(e => e.RefreshRateHz).HasColumnName("refresh_rate_hz");
            entity.Property(e => e.ResponseTimeMs).HasColumnName("response_time_ms");
            entity.Property(e => e.HdrSupport).HasColumnName("hdr_support");
            entity.Property(e => e.BrightnessNits).HasColumnName("brightness_nits");
            entity.Property(e => e.ContrastRatio).HasColumnName("contrast_ratio");
            entity.Property(e => e.ColorGamutP3).HasColumnName("color_gamut_p3");
            entity.Property(e => e.HasGSync).HasColumnName("has_gsync");
            entity.Property(e => e.HasFreeSync).HasColumnName("has_freesync");
            entity.Property(e => e.HasFreeSyncPremium).HasColumnName("has_freesync_premium");
            entity.Property(e => e.HdmiPorts).HasColumnName("inputs_hdmi");
            entity.Property(e => e.HdmiVersion).HasColumnName("hdmi_version");
            entity.Property(e => e.DpPorts).HasColumnName("inputs_dp");
            entity.Property(e => e.DpVersion).HasColumnName("dp_version");
            entity.Property(e => e.UsbCPorts).HasColumnName("inputs_usb_c");
            entity.Property(e => e.HasUsbHub).HasColumnName("has_usb_hub");
            entity.Property(e => e.HasSpeakers).HasColumnName("has_speakers");
            entity.Property(e => e.HeightAdjustable).HasColumnName("height_adjustable");
            entity.Property(e => e.VesaMount).HasColumnName("vesa_mount");
            entity.Property(e => e.PriceUsd).HasColumnName("price_usd");
        });

        modelBuilder.Entity<PsuEntity>(entity =>
        {
            ConfigureComponentColumnNames(entity);
            entity.Property(e => e.Wattage).HasColumnName("wattage");
            entity.Property(e => e.Efficiency).HasColumnName("efficiency");
            entity.Property(e => e.Modularity).HasColumnName("modular");
            entity.Property(e => e.AtxVersion).HasColumnName("atx_version");
            entity.Property(e => e.Has16Pin).HasColumnName("has_16pin");
            entity.Property(e => e.EpsConnectors).HasColumnName("eps_connectors");
            entity.Property(e => e.SataConnectors).HasColumnName("sata_connectors");
            entity.Property(e => e.Pcie8PinConnectors).HasColumnName("pcie_8pin");
            entity.Property(e => e.FanSizeMm).HasColumnName("fan_size_mm");
            entity.Property(e => e.LengthMm).HasColumnName("length_mm");
            entity.Property(e => e.PriceUsd).HasColumnName("price_usd");
        });

        modelBuilder.Entity<RamEntity>(entity =>
        {
            ConfigureComponentColumnNames(entity);
            entity.Property(e => e.MemoryType).HasColumnName("memory_type");
            entity.Property(e => e.CapacityGb).HasColumnName("capacity_gb");
            entity.Property(e => e.KitCount).HasColumnName("kit_count");
            entity.Property(e => e.SpeedMhz).HasColumnName("speed_mhz");
            entity.Property(e => e.CasLatency).HasColumnName("cas_latency");
            entity.Property(e => e.Voltage).HasColumnName("voltage");
            entity.Property(e => e.HasRgb).HasColumnName("has_rgb");
            entity.Property(e => e.HasEcc).HasColumnName("has_ecc");
            entity.Property(e => e.HeightMm).HasColumnName("height_mm");
            entity.Property(e => e.PriceUsd).HasColumnName("price_usd");
        });
    }

    private static void ConfigureComponentColumnNames<TComponent>(EntityTypeBuilder<TComponent> entity)
        where TComponent : Component
    {
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.BrandId).HasColumnName("brand_id");
        entity.Property(e => e.Name).HasColumnName("name");
    }

    private static void ConfigurePostgresEnumColumns(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CpuEntity>()
            .Property(c => c.Socket)
            .HasColumnType("socket_type");
        modelBuilder.Entity<CpuEntity>()
            .Property(c => c.MemoryType)
            .HasColumnType("memory_type");

        modelBuilder.Entity<CpuCoolerEntity>()
            .Property(c => c.CoolerType)
            .HasColumnType("cooler_type");

        modelBuilder.Entity<GpuEntity>()
            .Property(g => g.GpuInterface)
            .HasColumnType("gpu_interface");

        modelBuilder.Entity<HardDriveEntity>()
            .Property(h => h.DriveInterface)
            .HasColumnType("storage_interface");
        modelBuilder.Entity<HardDriveEntity>()
            .Property(h => h.FormFactor)
            .HasColumnType("storage_form_factor");

        modelBuilder.Entity<MotherboardEntity>()
            .Property(m => m.Socket)
            .HasColumnType("socket_type");
        modelBuilder.Entity<MotherboardEntity>()
            .Property(m => m.FormFactor)
            .HasColumnType("form_factor");
        modelBuilder.Entity<MotherboardEntity>()
            .Property(m => m.MemoryType)
            .HasColumnType("memory_type");

        modelBuilder.Entity<PcMonitorEntity>()
            .Property(m => m.PanelType)
            .HasColumnType("panel_type");

        modelBuilder.Entity<PsuEntity>()
            .Property(p => p.Efficiency)
            .HasColumnType("psu_rating");
        modelBuilder.Entity<PsuEntity>()
            .Property(p => p.Modularity)
            .HasColumnType("psu_modular");

        modelBuilder.Entity<RamEntity>()
            .Property(r => r.MemoryType)
            .HasColumnType("memory_type");
    }
    private static void ConfigureTablesForeignKeys(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuildEntity>()
           .HasOne(b => b.User)
           .WithMany()
           .HasForeignKey(b => b.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BuildEntity>()
            .HasOne(b => b.Cpu)
            .WithMany()
            .HasForeignKey(b => b.CpuId)
            .IsRequired(false);


        modelBuilder.Entity<BuildEntity>()
            .HasOne(b => b.Gpu)
            .WithMany()
            .HasForeignKey(b => b.GpuId)
            .IsRequired(false);


        modelBuilder.Entity<BuildEntity>()
            .HasOne(b => b.Motherboard)
            .WithMany()
            .HasForeignKey(b => b.MotherboardId)
            .IsRequired(false);


        modelBuilder.Entity<BuildEntity>()
            .HasOne(b => b.HardDrive)
            .WithMany()
            .HasForeignKey(b => b.HardDriveId)
            .IsRequired(false);


        modelBuilder.Entity<BuildEntity>()
            .HasOne(b => b.CpuCooler)
            .WithMany()
            .HasForeignKey(b => b.CpuCoolerId)
            .IsRequired(false);


        modelBuilder.Entity<BuildEntity>()
            .HasOne(b => b.Monitor)
            .WithMany()
            .HasForeignKey(b => b.MonitorId)
            .IsRequired(false);

        modelBuilder.Entity<BuildEntity>()
            .HasOne(b => b.Ram)
            .WithMany()
            .HasForeignKey(b => b.RamId)
            .IsRequired(false);

        modelBuilder.Entity<BuildEntity>()
            .HasOne(b => b.Psu)
            .WithMany()
            .HasForeignKey(b => b.PsuId)
            .IsRequired(false);

        modelBuilder.Entity<BuildEntity>()
            .HasOne(b => b.PcCase)
            .WithMany()
            .HasForeignKey(b => b.CaseId)
            .IsRequired(false);

        modelBuilder.Entity<CpuEntity>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<GpuEntity>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<PsuEntity>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<PcMonitorEntity>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<RamEntity>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<HardDriveEntity>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<PcCaseEntity>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<MotherboardEntity>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);

        modelBuilder.Entity<CpuCoolerEntity>()
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(c => c.BrandId);
    }
}
