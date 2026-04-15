using System.Diagnostics.Eventing.Reader;

namespace PcBuilder.Models
{
    public class Cpu : Component
    {
        
        
        public string? modelNumber { get; set; }
        public string socket { get; set; } = string.Empty;
        public string[] chipsetsSupported { get; set; } = [];
        public int cores { get; set; }
        public int threads { get; set; }
        public double baseClockGhz { get; set; }
        public double? boostClockGhz { get; set; }
        public int? l3CacheMb { get; set; }
        public int tdpWatts { get; set; }
        public string memoryType { get; set; } = string.Empty;
        public int maxMemoryGb { get; set; }
        public int maxMemorySpeedMhz { get; set; }
        public int memoryChannels { get; set; }
        public bool integratedGraphics { get; set; }
        public string? igpuModel { get; set; }
        public string? pcieVersion { get; set; }
        public int? pcieLanes { get; set; }
        public bool includesCooler { get; set; }
        public int? launchedYear { get; set; }
        public decimal? priceUsd { get; set; }

    }
}
