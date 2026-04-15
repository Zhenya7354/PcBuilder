namespace PcBuilder.Models
{
    public class Case : Component
    {
        public string[] supportedFormFactors { get; set; } = [];
        public int maxGpuLengthMm { get; set; }
        public int maxCpuCoolerHeightMm { get; set; }
        public int maxPsuLengthMm { get; set; }
        public int driveBays35Inch { get; set; }
        public int driveBays25Inch { get; set; }
        public int frontUsbA { get; set; }
        public int frontUsbC { get; set; }
        public string[]? radiatorSupportMm { get; set; } = [];
        public int? caseWidthMm { get; set; }
        public int? caseHeightMm { get; set; }
        public int? caseDepthMm { get; set; }
        public bool hasGlassPanel { get; set; }
        public int includedFans { get; set; }
        public decimal? priceUsd { get; set; }
    }
}
