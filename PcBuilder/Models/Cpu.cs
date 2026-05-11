using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Net.Sockets;

namespace PcBuilder.Models
{
    public class Cpu : Component
    {

        [Column("model_number")]
        public string? ModelNumber { get; set; }
        [Column("socket")]
        public PcSocketType Socket { get; set; }
        [Column("chipsets_supported")]
        public List<string> ChipsetsSupported { get; set; } = [];
        [Column("cores")]
        public int Cores { get; set; }
        public int Threads { get; set; }
        [Column("base_clock_ghz")]
        public double BaseClockGhz { get; set; }
        [Column("boost_clock_ghz")]
        public double? BoostClockGhz { get; set; }
        [Column("l3_cache_mb")]
        public int? L3CacheMb { get; set; }
        [Column("tdp_watts")]
        public int TdpWatts { get; set; }
        [Column("memory_type")]
        public MemoryType MemoryType { get; set; }
        [Column("max_memory_gb")]
        public int MaxMemoryGb { get; set; }
        [Column("max_memory_speed_mhz")]
        public int MaxMemorySpeedMhz { get; set; }
        [Column("memory_channels")]
        public int MemoryChannels { get; set; }
        [Column("integrated_graphics")]
        public bool IntegratedGraphics { get; set; }
        [Column("igpu_model")]
        public string? IgpuModel { get; set; }
        [Column("pcie_version")]
        public string? PcieVersion { get; set; }
        [Column("pcie_lanes")]
        public int? PcieLanes { get; set; }
        [Column("includes_cooler")]
        public bool IncludesCooler { get; set; }
        [Column("launched_year")]
        public int? LaunchedYear { get; set; }
        [Column("price_usd")]
        public decimal? PriceUsd { get; set; }

    }
}
