namespace PcBuilder.Models
{
    public class CpuCooler : Component
    {
        
        public string coolerType { get; set; } = string.Empty;
        public string[] socketsSupported { get; set; } = [];
        public int? radiatorSizeMm { get; set; }
        public int fanCount { get; set; }
        public int fanSizeMm { get; set; }
        public int maxTdpWatts { get; set; }
        public int? heightMm { get; set; }
        public bool hasRgb { get; set; }
        public double? noiseLevelDb { get; set; }
        public decimal? priceUsd { get; set; }
    }
}
