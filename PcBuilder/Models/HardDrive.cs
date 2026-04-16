namespace PcBuilder.Models
{
    public class HardDrive : Component
    {
        public int capacityGb { get; set; }
        public string driveInterface { get; set; } = string.Empty;
        public string formFactor { get; set; } = string.Empty;
        public bool isSsd { get; set; }
        public int? readSpeedMbS { get; set; }
        public int? writeSpeedMbs { get; set; }
        public int? rpm { get; set; }
        public int? cacheMb { get; set; }
        public int? tbw { get; set; }
        public double? powerWatts { get; set; }
        public decimal? priceUsd { get; set; }
    }
}
