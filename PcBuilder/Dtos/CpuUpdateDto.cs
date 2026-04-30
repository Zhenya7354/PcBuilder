using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos
{
    public record CpuUpdateDto
        (
        string? Name,
        int? BrandId,
        string? ModelNumber,
        PcSocketType? Socket,
        List<string>? ChipsetsSupported,
        [Range(1, 128)] int? Cores,
        [Range(1, 256)] int? Threads,
        [Range(0.5, 10.0)] double? BaseClockGhz,
        double? BoostClockGhz,
        int? L3CacheMb,
        [Range(1, 1000)] int? TdpWatts,
        MemoryType? MemoryType,
        [Range(1, 2048)] int? MaxMemoryGb,
        [Range(800, 10000)] int? MaxMemorySpeedMhz,
        [Range(1, 8)] int? MemoryChannels,
        bool? IntegratedGraphics,
        string? IgpuModel,
        string? PcieVersion,
        int? PcieLanes,
        bool? IncludesCooler,
        int? LaunchedYear,
        [Range(0, 100000)] decimal? PriceUsd
        );
}
