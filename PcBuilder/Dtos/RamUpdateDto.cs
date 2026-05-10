using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos
{
    public record RamUpdateDto(
        string? Name,
        int? BrandId,
        MemoryType? MemoryType,
        [Range(1, 2048)] int? CapacityGb,
        [Range(1, 16)] int? KitCount,
        [Range(800, 10000)] int? SpeedMhz,
        int? CasLatency,
        double? Voltage,
        bool? HasRgb,
        bool? HasEcc,
        int? HeightMm,
        [Range(0, 100000)] decimal? PriceUsd
    );
}
