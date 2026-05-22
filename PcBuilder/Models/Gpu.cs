using PcBuilder.Enums;

namespace PcBuilder.Models;

public class Gpu : Component
{
    public string GpuChip { get; set; } = string.Empty;
    public GpuInterface GpuInterface { get; set; }
    public int VramGb { get; set; }
    public string VramType { get; set; } = string.Empty;
    public int? BaseClockMhz { get; set; }
    public int BoostClockMhz { get; set; }
    public int MemoryBusBits { get; set; }
    public double? MemoryBandwithGb { get; set; }
    public int TdpWatts { get; set; }
    public int RecommendedPsuWattage { get; set; }
    public string? PowerConnectors { get; set; }
    public int OutputHdmi { get; set; }
    public int OutputDp { get; set; }
    public int? CardLengthMm { get; set; }
    public double CardSlots { get; set; }
    public bool HasRgb { get; set; }
    public decimal? Price { get; set; }

}