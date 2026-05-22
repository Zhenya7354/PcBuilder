using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Entities;

public class PsuEntity : Component
{
    public int Wattage { get; set; }
    public PsuRating Efficiency { get; set; }
    public PsuModular Modularity { get; set; }
    public string AtxVersion { get; set; } = string.Empty;
    public bool Has16Pin { get; set; }
    public int EpsConnectors { get; set; }
    public int SataConnectors { get; set; }
    public int Pcie8PinConnectors { get; set; }
    public int FanSizeMm { get; set; }
    public int? LengthMm { get; set; }
    public decimal? PriceUsd { get; set; }


}