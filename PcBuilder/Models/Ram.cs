using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class Ram : Component
    {
        [Column("memory_type")]
        public MemoryType MemoryType { get; set; }
        [Column("capacity_gb")]
        public int CapacityGb { get; set; }
        [Column("kit_count")]
        public int KitCount { get; set; }
        [Column("speed_mhz")]
        public int SpeedMhz { get; set; }
        [Column("cas_latency")]
        public int? CasLatency { get; set; }
        [Column("voltage")]
        public double? Voltage { get; set; }
        [Column("has_rgb")]
        public bool HasRgb { get; set; }
        [Column("has_ecc")]
        public bool HasEcc { get; set; }
        [Column("height_mm")]
        public int? HeightMm { get; set; }
        [Column("price_usd")]
        public decimal? PriceUsd { get; set; }
    }
}
