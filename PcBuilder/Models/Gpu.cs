using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class Gpu : Component
    {
        [Column("grpu_chip")]
        public string gpuChip { get; set; } = string.Empty;
        [Column("interface")]
        public GpuInterface gpuInterface { get; set; }
        [Column("vram_gb")]
        public int vram_gb { get; set; }
        [Column("vram_type")]
        public string vramType { get; set; } = string.Empty;
        [Column("base_clock_mhz")]
        public int? baseClockMhz { get; set; }
        [Column("boost_clock_mhz")]
        public int boostClockMhz { get; set; }
        [Column("memory_bus_bits")]
        public int memoryBusBits { get; set; }
        [Column("memory_bandwidth_gb")]
        public double? memoryBandwithGb { get; set; }
        [Column("tdp_watts")]
        public int tdpWatts { get; set; }
        [Column("recommended_psu_wattage")]
        public int recommendedPsuWattage { get; set; }
        [Column("power_connectors")]
        public string? powerConnectors { get; set; }
        [Column("output_hdmi")]
        public int outputHdmi { get; set; }
        [Column("output_dp")]
        public int outputDp { get; set; }
        [Column("card_length_mm")]
        public int? cardLengthMm { get; set; }
        [Column("cord_slots")]
        public double cordSlots { get; set; }
        [Column("has_rgb")]
        public bool hasRgb { get; set; }
        [Column("price")]
        public decimal? price { get; set; }

    }
}
