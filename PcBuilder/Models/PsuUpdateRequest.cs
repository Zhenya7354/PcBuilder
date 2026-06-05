using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record PsuUpdateRequest(
    string? Name,
    int? BrandId,
    [Range(1, 3000)] int? Wattage,
    PsuRating? Efficiency,
    PsuModular? Modularity,
    string? AtxVersion,
    bool? Has16Pin,
    [Range(0, 10)] int? EpsConnectors,
    [Range(0, 30)] int? SataConnectors,
    [Range(0, 20)] int? Pcie8PinConnectors,
    [Range(40, 300)] int? FanSizeMm,
    int? LengthMm,
    Currency? Currency,
    [Range(0, 100000)] decimal? Price
);
