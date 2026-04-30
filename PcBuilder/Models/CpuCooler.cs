using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class CpuCooler : Component
    {
        [Column("cooler_type")]
        public CoolerType coolerType { get; set; }
        [Column("sockets_supported")]
        public string[] socketsSupported { get; set; } = [];
        [Column("radiator_size_mm")]
        public int? radiatorSizeMm { get; set; }
        [Column("fan_count")]
        public int fanCount { get; set; }
        [Column("fan_size_mm")]
        public int fanSizeMm { get; set; }
        [Column("max_tdp_watts")]
        public int maxTdpWatts { get; set; }
        [Column("height_mm")]
        public int? heightMm { get; set; }
        [Column("has_rgb")]
        public bool hasRgb { get; set; }
        [Column("noise_db")]
        public double? noiseLevelDb { get; set; }
        [Column("price_usd")]
        public decimal? priceUsd { get; set; }
    }
}
