using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record CpuCoolerCreateRequest(
    [Required] string Name,
    [Required] int BrandId,
    [Required] CoolerType CoolerType,
    List<PcSocketType> SocketsSupported,
    int? RadiatorSizeMm,
    [Required, Range(1, 10)] int FanCount,
    [Required, Range(40, 300)] int FanSizeMm,
    [Required, Range(1, 1000)] int MaxTdpWatts,
    int? HeightMm,
    [Required] ColorScheme ColorScheme,
    double? NoiseLevelDb,
    [Required] Currency? Currency,
    [Range(0, 100000)] decimal? Price
);
