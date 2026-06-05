using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record CpuCreateRequest
    (
    [Required] string Name,
    [Required] int BrandId,
    string? ModelNumber,
    [Required] PcSocketType Socket,
    List<string> ChipsetsSupported,
    [Required, Range(1, 128)] int Cores,
    [Required, Range(1, 256)] int Threads,
    [Required, Range(0.5, 10.0)] double BaseClockGhz,
    double? BoostClockGhz,
    int? L3CacheMb,
    [Required, Range(1, 1000)] int TdpWatts,
    [Required] MemoryType MemoryType,
    [Required, Range(1, 2048)] int MaxMemoryGb,
    [Required, Range(800, 10000)] int MaxMemorySpeedMhz,
    [Required, Range(1, 8)] int MemoryChannels,
    bool IntegratedGraphics,
    string? IgpuModel,
    string? PcieVersion,
    int? PcieLanes,
    bool IncludesCooler,
    int? LaunchedYear,
    [Required] Currency? Currency,
    [Range(0, 100000)] decimal? Price
    );
