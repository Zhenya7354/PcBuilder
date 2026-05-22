using PcBuilder.Enums;
using System.Diagnostics.Eventing.Reader;
using System.Net.Sockets;

namespace PcBuilder.Models;

public class Cpu : Component
{

    public string? ModelNumber { get; set; }
    public PcSocketType Socket { get; set; }
    public List<string> ChipsetsSupported { get; set; } = [];
    public int Cores { get; set; }
    public int Threads { get; set; }
    public double BaseClockGhz { get; set; }
    public double? BoostClockGhz { get; set; }
    public int? L3CacheMb { get; set; }
    public int TdpWatts { get; set; }
    public MemoryType MemoryType { get; set; }
    public int MaxMemoryGb { get; set; }
    public int MaxMemorySpeedMhz { get; set; }
    public int MemoryChannels { get; set; }
    public bool IntegratedGraphics { get; set; }
    public string? IgpuModel { get; set; }
    public string? PcieVersion { get; set; }
    public int? PcieLanes { get; set; }
    public bool IncludesCooler { get; set; }
    public int? LaunchedYear { get; set; }
    public decimal? PriceUsd { get; set; }

}