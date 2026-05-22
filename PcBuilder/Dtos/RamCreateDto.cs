using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos;

public record RamCreateDto(
    [Required] string Name,
    [Required] int BrandId,
    [Required] MemoryType MemoryType,
    [Required, Range(1, 2048)] int CapacityGb,
    [Required, Range(1, 16)] int KitCount,
    [Required, Range(800, 10000)] int SpeedMhz,
    int? CasLatency,
    double? Voltage,
    bool HasRgb,
    bool HasEcc,
    int? HeightMm,
    [Range(0, 100000)] decimal? PriceUsd
);
