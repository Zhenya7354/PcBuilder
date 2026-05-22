using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos;

public record PcCaseCreateDto(
    [Required] string Name,
    [Required] int BrandId,
    List<FormFactor> SupportedFormFactors,
    [Required, Range(1, 1000)] int MaxGpuLengthMm,
    [Required, Range(1, 300)] int MaxCpuCoolerHeightMm,
    [Required, Range(1, 500)] int MaxPsuLengthMm,
    [Required, Range(0, 20)] int DriveBays35Inch,
    [Required, Range(0, 20)] int DriveBays25Inch,
    [Required, Range(0, 20)] int FrontUsbA,
    [Required, Range(0, 20)] int FrontUsbC,
    List<string> RadiatorSupportMm,
    int? CaseWidthMm,
    int? CaseHeightMm,
    int? CaseDepthMm,
    bool HasGlassPanel,
    [Required, Range(0, 20)] int IncludedFans,
    [Range(0, 100000)] decimal? PriceUsd
);
