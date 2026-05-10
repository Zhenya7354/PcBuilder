using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos
{
    public record CpuCoolerCreateDto(
        [Required] string Name,
        [Required] int BrandId,
        [Required] CoolerType CoolerType,
        List<string> SocketsSupported,
        int? RadiatorSizeMm,
        [Required, Range(1, 10)] int FanCount,
        [Required, Range(40, 300)] int FanSizeMm,
        [Required, Range(1, 1000)] int MaxTdpWatts,
        int? HeightMm,
        bool HasRgb,
        double? NoiseLevelDb,
        [Range(0, 100000)] decimal? PriceUsd
    );
}
