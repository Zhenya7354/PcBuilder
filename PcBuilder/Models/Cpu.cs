using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Net.Sockets;

namespace PcBuilder.Models
{
    public class Cpu : Component
    {

        [Column("model_number")]
        public string? modelNumber { get; set; }
        [Column("socket")]
        public PcSocketType socket { get; set; }
        [Column("chipsets_supported")]
        public List<string> chipsetsSupported { get; set; } = [];
        [Column("cores")]
        public int cores { get; set; }
        public int threads { get; set; }
        [Column("base_clock_ghz")]
        public double baseClockGhz { get; set; }
        [Column("boost_clock_ghz")]
        public double? boostClockGhz { get; set; }
        [Column("l3_cache_mb")]
        public int? l3CacheMb { get; set; }
        [Column("tdp_watts")]
        public int tdpWatts { get; set; }
        [Column("memory_type")]
        public MemoryType memoryType { get; set; }
        [Column("max_memory_gb")]
        public int maxMemoryGb { get; set; }
        [Column("max_memory_speed_mhz")]
        public int maxMemorySpeedMhz { get; set; }
        [Column("memory_channels")]
        public int memoryChannels { get; set; }
        [Column("integrated_graphics")]
        public bool integratedGraphics { get; set; }
        [Column("igpu_model")]
        public string? igpuModel { get; set; }
        [Column("pcie_version")]
        public string? pcieVersion { get; set; }
        [Column("pcie_lanes")]
        public int? pcieLanes { get; set; }
        [Column("includes_cooler")]
        public bool includesCooler { get; set; }
        [Column("launched_year")]
        public int? launchedYear { get; set; }
        [Column("price_usd")]
        public decimal? priceUsd { get; set; }

    }
}
