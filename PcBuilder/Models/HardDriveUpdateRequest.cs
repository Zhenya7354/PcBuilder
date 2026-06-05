using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record HardDriveUpdateRequest(
    string? Name,
    int? BrandId,
    [Range(1, 1000000)] int? CapacityGb,
    StorageInterface? DriveInterface,
    StorageFormFactor? FormFactor,
    PcDriveType? PcDriveType,
    int? ReadSpeedMbS,
    int? WriteSpeedMbs,
    int? Rpm,
    int? CacheMb,
    int? Tbw,
    double? PowerWatts,
    Currency? Currency,
    [Range(0, 100000)] decimal? Price
);
