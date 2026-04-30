using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class Ram : Component
    {
        [Column("memory_type")]
        public MemoryType memoryType { get; set; }
        [Column("capacity_gb")]
        public int capacityGb { get; set; }
        [Column("kit_count")]
        public int kitCount { get; set; }
        [Column("speed_mhz")]
        public int speedMhz { get; set; }
        [Column("cas_latency")]
        public int? casLatency { get; set; }
        [Column("voltage")]
        public double? voltage { get; set; }
        [Column("has_rgb")]
        public bool hasRgb { get; set; }
        [Column("has_ecc")]
        public bool hasEcc { get; set; }
        [Column("height_mm")]
        public int? heightMm { get; set; }
        [Column("price_usd")]
        public decimal? priceUsd { get; set; }
    }
}
