using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models;

public class HardDrive : Component
{
    [Column("capacity_gb")]
    public int CapacityGb { get; set; }
    [Column("interface")]
    public StorageInterface DriveInterface { get; set; }
    [Column("form_factor")]
    public StorageFormFactor FormFactor { get; set; }
    [Column("is_ssd")]
    public bool IsSsd { get; set; }
    [Column("read_speed_mbs")]
    public int? ReadSpeedMbS { get; set; }
    [Column("write_speed_mbs")]
    public int? WriteSpeedMbs { get; set; }
    [Column("rpm")]
    public int? Rpm { get; set; }
    [Column("cache_mb")]
    public int? CacheMb { get; set; }
    [Column("tbw")]
    public int? Tbw { get; set; }
    [Column("power_watts")]
    public double? PowerWatts { get; set; }
    [Column("price_usd")]
    public decimal? PriceUsd { get; set; }
}
