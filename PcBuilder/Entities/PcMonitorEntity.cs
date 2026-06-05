using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Entities;

public class PcMonitorEntity : Component
{
    public double ScreenSizeInch { get; set; }
    public int ResolutionWidth { get; set; }
    public int ResolutionHeight { get; set; }
    public PanelType PanelType { get; set; }
    public int RefreshRateHz { get; set; }
    public double? ResponseTimeMs { get; set; }
    public string? HdrSupport { get; set; }
    public int? BrightnessNits { get; set; }
    public string? ContrastRatio { get; set; }
    public int? ColorGamutP3 { get; set; }
    public bool HasGSync { get; set; }
    public bool HasFreeSync { get; set; }
    public bool HasFreeSyncPremium { get; set; }
    public int HdmiPorts { get; set; }
    public string? HdmiVersion { get; set; }
    public int DpPorts { get; set; }
    public string? DpVersion { get; set; }
    public int UsbCPorts { get; set; }
    public bool HasUsbHub { get; set; }
    public bool HasSpeakers { get; set; }
    public bool HeightAdjustable { get; set; }
    public string VesaMount { get; set; } = string.Empty;
    public Currency? Currency { get; set; }
    public decimal? Price { get; set; }
}