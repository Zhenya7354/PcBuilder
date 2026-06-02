using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record HardDriveCreateRequest(
    [Required] string Name,
    [Required] int BrandId,
    [Required, Range(1, 1000000)] int CapacityGb,
    [Required] StorageInterface DriveInterface,
    [Required] StorageFormFactor FormFactor,
    bool IsSsd,
    int? ReadSpeedMbS,
    int? WriteSpeedMbs,
    int? Rpm,
    int? CacheMb,
    int? Tbw,
    double? PowerWatts,
    [Range(0, 100000)] decimal? PriceUsd
);
