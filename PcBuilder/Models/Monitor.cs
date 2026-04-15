namespace PcBuilder.Models
{
    public class Monitor : Component
    {
        public double screenSizeInch { get; set; }
        public int resolutionWidth { get; set; }
        public int resolutionHeight { get; set; }
        public string panelType { get; set; } = string.Empty;
        public int refreshRateHz { get; set; }
        public double? responseTimeMs { get; set; }
        public string? hdrSupport { get; set; }
        public int? brightnessNits { get; set; }
        public string? contrastRatio { get; set; }
        public int? colorGamutP3 { get; set; }
        public bool hasGSync { get; set; }
        public bool hasFreeSync { get; set; }
        public bool hasFreeSyncPremium { get; set; }
        public int hdmiPorts { get; set; }
        public string? hdmiVersion { get; set; }
        public int dpPorts { get; set; }
        public string? dpVersion { get; set; }
        public int usbCPorts { get; set; }
        public bool hasUsbHub { get; set; }
        public bool hasSpeakers { get; set; }
        public bool heightAdjustable { get; set; }
        public string vesaMount { get; set; } = string.Empty;
        public decimal? priceUsd { get; set; }
    }
}
