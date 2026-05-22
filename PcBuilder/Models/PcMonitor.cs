using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models;

public class PcMonitor : Component
{
    [Column("screen_size_inch")]
    public double ScreenSizeInch { get; set; }
    [Column("resolution_w")]
    public int ResolutionWidth { get; set; }
    [Column("resolution_h")]
    public int ResolutionHeight { get; set; }
    [Column("panel_type")]
    public PanelType PanelType { get; set; }
    [Column("refresh_rate_hz")]
    public int RefreshRateHz { get; set; }
    [Column("response_time_ms")]
    public double? ResponseTimeMs { get; set; }
    [Column("hdr_support")]
    public string? HdrSupport { get; set; }
    [Column("brightness_nits")]
    public int? BrightnessNits { get; set; }
    [Column("contrast_ratio")]
    public string? ContrastRatio { get; set; }
    [Column("color_gamut_p3")]
    public int? ColorGamutP3 { get; set; }
    [Column("has_gsync")]
    public bool HasGSync { get; set; }
    [Column("has_freesync")]
    public bool HasFreeSync { get; set; }
    [Column("has_freesync_premium")]
    public bool HasFreeSyncPremium { get; set; }
    [Column("inputs_hdmi")]
    public int HdmiPorts { get; set; }
    [Column("hdmi_version")]
    public string? HdmiVersion { get; set; }
    [Column("inputs_dp")]
    public int DpPorts { get; set; }
    [Column("dp_version")]
    public string? DpVersion { get; set; }
    [Column("inputs_usb_c")]
    public int UsbCPorts { get; set; }
    [Column("has_usb_hub")]
    public bool HasUsbHub { get; set; }
    [Column("has_speakers")]
    public bool HasSpeakers { get; set; }
    [Column("height_adjustable")]
    public bool HeightAdjustable { get; set; }
    [Column("vesa_mount")]
    public string VesaMount { get; set; } = string.Empty;
    [Column("price_usd")]
    public decimal? PriceUsd { get; set; }
}
