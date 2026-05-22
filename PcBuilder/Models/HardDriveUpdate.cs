using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record HardDriveUpdate(
    string? Name,
    int? BrandId,
    [Range(1, 1000000)] int? CapacityGb,
    StorageInterface? DriveInterface,
    StorageFormFactor? FormFactor,
    bool? IsSsd,
    int? ReadSpeedMbS,
    int? WriteSpeedMbs,
    int? Rpm,
    int? CacheMb,
    int? Tbw,
    double? PowerWatts,
    [Range(0, 100000)] decimal? PriceUsd
);
