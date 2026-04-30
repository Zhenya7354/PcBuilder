using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class HardDrive : Component
    {
        [Column("capacity_gb")]
        public int capacityGb { get; set; }
        [Column("interface")]
        public StorageInterface driveInterface { get; set; }
        [Column("form_factor")]
        public StorageFormFactor formFactor { get; set; }
        [Column("is_ssd")]
        public bool isSsd { get; set; }
        [Column("read_speed_mbps")]
        public int? readSpeedMbS { get; set; }
        [Column("write_speed_mbps")]
        public int? writeSpeedMbs { get; set; }
        public int? rpm { get; set; }
        [Column("cache_mb")]
        public int? cacheMb { get; set; }
        public int? tbw { get; set; }
        [Column("power_watts")]
        public double? powerWatts { get; set; }
        [Column("price_usd")]
        public decimal? priceUsd { get; set; }
    }
}
