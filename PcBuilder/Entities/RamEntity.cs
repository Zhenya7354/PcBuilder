using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Entities;

public class RamEntity : Component
{
    public MemoryType MemoryType { get; set; }
    public int CapacityGb { get; set; }
    public int KitCount { get; set; }
    public int SpeedMhz { get; set; }
    public int? CasLatency { get; set; }
    public double? Voltage { get; set; }
    public bool HasRgb { get; set; }
    public bool HasEcc { get; set; }
    public int? HeightMm { get; set; }
    public decimal? PriceUsd { get; set; }
}