namespace PcBuilder.Models
{
    public class Gpu : Component
    {
        
        public string gpuChip { get; set; } = string.Empty;
        public string gpuInterface { get; set; } = string.Empty;
        public int vram_gb { get; set; }
        public string vramType { get; set; } = string.Empty;
        public int? baseClockMhz { get; set; }
        public int boostClockMhz { get; set; }
        public int memoryBusBits { get; set; }
        public double? memoryBandwithGb { get; set; }
        public int tdpWatts { get; set; }
        public int recommendedPsuWattage { get; set; }
        public string? powerConnectors { get; set; }
        public int outputHdmi { get; set; }
        public int outputDp { get; set; }
        public int? cardLengthMm { get; set; }
        public double cordSlots { get; set; }
        public bool hasRgb { get; set; }
        public decimal? price { get; set; }

    }
}
