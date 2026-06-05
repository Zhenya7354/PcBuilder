using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record RamUpdateRequest(
    string? Name,
    int? BrandId,
    MemoryType? MemoryType,
    [Range(1, 2048)] int? CapacityGb,
    [Range(1, 16)] int? KitCount,
    [Range(800, 10000)] int? SpeedMhz,
    int? CasLatency,
    double? Voltage,
    ColorScheme? ColorScheme,
    bool? HasEcc,
    int? HeightMm,
    Currency? Currency,
    [Range(0, 100000)] decimal? Price
);
