using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record PcCaseUpdateRequest(
    string? Name,
    int? BrandId,
    List<FormFactor>? SupportedFormFactors,
    [Range(1, 1000)] int? MaxGpuLengthMm,
    [Range(1, 300)] int? MaxCpuCoolerHeightMm,
    [Range(1, 500)] int? MaxPsuLengthMm,
    [Range(0, 20)] int? DriveBays35Inch,
    [Range(0, 20)] int? DriveBays25Inch,
    [Range(0, 20)] int? FrontUsbA,
    [Range(0, 20)] int? FrontUsbC,
    List<string>? RadiatorSupportMm,
    int? CaseWidthMm,
    int? CaseHeightMm,
    int? CaseDepthMm,
    bool? HasGlassPanel,
    [Range(0, 20)] int? IncludedFans,
    [Range(0, 100000)] decimal? PriceUsd
);
