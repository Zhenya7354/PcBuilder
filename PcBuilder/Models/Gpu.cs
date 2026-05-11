using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class Gpu : Component
    {
        [Column("gpu_chip")]
        public string GpuChip { get; set; } = string.Empty;
        [Column("interface")]
        public GpuInterface GpuInterface { get; set; }
        [Column("vram_gb")]
        public int VramGb { get; set; }
        [Column("vram_type")]
        public string VramType { get; set; } = string.Empty;
        [Column("base_clock_mhz")]
        public int? BaseClockMhz { get; set; }
        [Column("boost_clock_mhz")]
        public int BoostClockMhz { get; set; }
        [Column("memory_bus_bits")]
        public int MemoryBusBits { get; set; }
        [Column("memory_bandwidth_gbps")]
        public double? MemoryBandwithGb { get; set; }
        [Column("tdp_watts")]
        public int TdpWatts { get; set; }
        [Column("recommended_psu_w")]
        public int RecommendedPsuWattage { get; set; }
        [Column("power_connectors")]
        public string? PowerConnectors { get; set; }
        [Column("outputs_hdmi")]
        public int OutputHdmi { get; set; }
        [Column("outputs_dp")]
        public int OutputDp { get; set; }
        [Column("card_length_mm")]
        public int? CardLengthMm { get; set; }
        [Column("card_slots")]
        public double CardSlots { get; set; }
        [Column("has_rgb")]
        public bool HasRgb { get; set; }
        [Column("price_usd")]
        public decimal? Price { get; set; }

    }
}
