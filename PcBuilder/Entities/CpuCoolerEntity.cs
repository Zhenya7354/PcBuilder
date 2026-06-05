using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Entities;

public class CpuCoolerEntity : Component
{
    public CoolerType CoolerType { get; set; }
    public List<PcSocketType> SocketsSupported { get; set; } = [];
    public int? RadiatorSizeMm { get; set; }
    public int FanCount { get; set; }
    public int FanSizeMm { get; set; }
    public int MaxTdpWatts { get; set; }
    public int? HeightMm { get; set; }
    public ColorScheme ColorScheme { get; set; }
    public double? NoiseLevelDb { get; set; }
    public Currency? Currency { get; set; }
    public decimal? Price { get; set; }
}