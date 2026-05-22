using PcBuilder.Enums;

namespace PcBuilder.Models;

public class HardDrive : Component
{
    public int CapacityGb { get; set; }
    public StorageInterface DriveInterface { get; set; }
    public StorageFormFactor FormFactor { get; set; }
    public bool IsSsd { get; set; }
    public int? ReadSpeedMbS { get; set; }
    public int? WriteSpeedMbs { get; set; }
    public int? Rpm { get; set; }
    public int? CacheMb { get; set; }
    public int? Tbw { get; set; }
    public double? PowerWatts { get; set; }
    public decimal? PriceUsd { get; set; }
}