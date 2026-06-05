using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record PcMonitorCreateRequest(
    [Required] string Name,
    [Required] int BrandId,
    [Required, Range(1, 100)] double ScreenSizeInch,
    [Required, Range(1, 10000)] int ResolutionWidth,
    [Required, Range(1, 10000)] int ResolutionHeight,
    [Required] PanelType PanelType,
    [Required, Range(1, 1000)] int RefreshRateHz,
    double? ResponseTimeMs,
    string? HdrSupport,
    int? BrightnessNits,
    string? ContrastRatio,
    int? ColorGamutP3,
    bool HasGSync,
    bool HasFreeSync,
    bool HasFreeSyncPremium,
    [Required, Range(0, 20)] int HdmiPorts,
    string? HdmiVersion,
    [Required, Range(0, 20)] int DpPorts,
    string? DpVersion,
    [Required, Range(0, 20)] int UsbCPorts,
    bool HasUsbHub,
    bool HasSpeakers,
    bool HeightAdjustable,
    [Required] string VesaMount,
    Currency? Currency,
    [Range(0, 100000)] decimal? Price
);
