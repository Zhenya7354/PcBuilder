using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models
{
    public class PcMonitor : Component
    {
        [Column("screen_size_inch")]
        public double screenSizeInch { get; set; }
        [Column("resolution_width")]
        public int resolutionWidth { get; set; }
        [Column("resolution_height")]
        public int resolutionHeight { get; set; }
        [Column("panel_type")]
        public PanelType panelType { get; set; }
        [Column("refresh_rate_hz")]
        public int refreshRateHz { get; set; }
        [Column("response_time_ms")]
        public double? responseTimeMs { get; set; }
        [Column("hdr_support")]
        public string? hdrSupport { get; set; }
        [Column("brightness_nits")]
        public int? brightnessNits { get; set; }
        [Column("contrast_ratio")]
        public string? contrastRatio { get; set; }
        [Column("color_gamut_p3")]
        public int? colorGamutP3 { get; set; }
        [Column("has_gsync")]
        public bool hasGSync { get; set; }
        [Column("has_freesync")]
        public bool hasFreeSync { get; set; }
        [Column("has_freesync_premium")]
        public bool hasFreeSyncPremium { get; set; }
        [Column("inputs_hdmi")]
        public int hdmiPorts { get; set; }
        [Column("hdmi_version")]
        public string? hdmiVersion { get; set; }
        [Column("inputs_dp")]
        public int dpPorts { get; set; }
        [Column("dp_version")]
        public string? dpVersion { get; set; }
        [Column("inputs_usb_c")]
        public int usbCPorts { get; set; }
        [Column("has_usb_hub")]
        public bool hasUsbHub { get; set; }
        [Column("has_speakers")]
        public bool hasSpeakers { get; set; }
        [Column("height_adjustable")]
        public bool heightAdjustable { get; set; }
        [Column("vesa_mount")]
        public string vesaMount { get; set; } = string.Empty;
        [Column("price_usd")]
        public decimal? priceUsd { get; set; }
    }
}
