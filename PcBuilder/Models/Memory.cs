namespace PcBuilder.Models
{
    public class Memory : Component
    {
        public string memoryType { get; set; } = string.Empty;
        public int capacityGb { get; set; }
        public int kitCount { get; set; }
        public int speedMhz { get; set; }
        public int? casLatency { get; set; }
        public double? voltage { get; set; }
        public bool hasRgb { get; set; }
        public bool hasEcc { get; set; }
        public int? heightMm { get; set; }
        public decimal? priceUsd { get; set; }
    }
}
