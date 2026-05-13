using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class CpuCooler : Component
    {
        [Column("cooler_type")]
        public CoolerType CoolerType { get; set; }
        [Column("socket_support")]
        public List<PcSocketType> SocketsSupported { get; set; } = [];
        [Column("radiator_size_mm")]
        public int? RadiatorSizeMm { get; set; }
        [Column("fan_count")]
        public int FanCount { get; set; }
        [Column("fan_size_mm")]
        public int FanSizeMm { get; set; }
        [Column("max_tdp_watts")]
        public int MaxTdpWatts { get; set; }
        [Column("height_mm")]
        public int? HeightMm { get; set; }
        [Column("has_rgb")]
        public bool HasRgb { get; set; }
        [Column("noise_db")]
        public double? NoiseLevelDb { get; set; }
        [Column("price_usd")]
        public decimal? PriceUsd { get; set; }
    }
}
