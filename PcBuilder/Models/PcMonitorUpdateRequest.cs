using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record PcMonitorUpdateRequest(
    string? Name,
    int? BrandId,
    [Range(1, 100)] double? ScreenSizeInch,
    [Range(1, 10000)] int? ResolutionWidth,
    [Range(1, 10000)] int? ResolutionHeight,
    PanelType? PanelType,
    [Range(1, 1000)] int? RefreshRateHz,
    double? ResponseTimeMs,
    string? HdrSupport,
    int? BrightnessNits,
    string? ContrastRatio,
    int? ColorGamutP3,
    bool? HasGSync,
    bool? HasFreeSync,
    bool? HasFreeSyncPremium,
    [Range(0, 20)] int? HdmiPorts,
    string? HdmiVersion,
    [Range(0, 20)] int? DpPorts,
    string? DpVersion,
    [Range(0, 20)] int? UsbCPorts,
    bool? HasUsbHub,
    bool? HasSpeakers,
    bool? HeightAdjustable,
    string? VesaMount,
    [Range(0, 100000)] decimal? PriceUsd
);
