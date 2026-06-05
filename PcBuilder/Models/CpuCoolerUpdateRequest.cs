using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record CpuCoolerUpdateRequest(
    string? Name,
    int? BrandId,
    CoolerType? CoolerType,
    List<PcSocketType>? SocketsSupported,
    int? RadiatorSizeMm,
    [Range(1, 10)] int? FanCount,
    [Range(40, 300)] int? FanSizeMm,
    [Range(1, 1000)] int? MaxTdpWatts,
    int? HeightMm,
    ColorScheme? ColorScheme,
    double? NoiseLevelDb,
    Currency? Currency,
    [Range(0, 100000)] decimal? Price
);
