using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos
{
    public record CpuCoolerUpdateDto(
        string? Name,
        int? BrandId,
        CoolerType? CoolerType,
        List<string> SocketsSupported,
        int? RadiatorSizeMm,
        [Range(1, 10)] int? FanCount,
        [Range(40, 300)] int? FanSizeMm,
        [Range(1, 1000)] int? MaxTdpWatts,
        int? HeightMm,
        bool? HasRgb,
        double? NoiseLevelDb,
        [Range(0, 100000)] decimal? PriceUsd
    );
}
