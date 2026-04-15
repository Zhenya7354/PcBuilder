namespace PcBuilder.Models
{
    public class Psu : Component
    {
        public int wattage { get; set; }
        public string efficiency { get; set; } = string.Empty;
        public string modularity { get; set; } = string.Empty;
        public string atxVersion { get; set; } = string.Empty;
        public bool has16Pin { get; set; }
        public int epsConnectors { get; set; }
        public int sataConnectors { get; set; }
        public int pcie8PinConnectors { get; set; }
        public int fanSizeMm { get; set; }
        public int? lengthMm { get; set; }
        public decimal? priceUsd { get; set; }


    }
}
