using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcBuilder.Models;

public record GpuCreateRequest
(
    [Required] string Name,
    [Required] int BrandId,
    [Required] string gpuChip,
    [Required] GpuInterface gpuInterface,
    [Required, Range(1, 50)] int vram_gb,
    [Required] string vramType,
     int? baseClockMhz,
    [Required] int boostClockMhz,
    [Required] int memoryBusBits,
     double? memoryBandwithGb,
    [Required] int tdpWatts,
    [Required, Range(20, 2000)] int recommendedPsuWattage,
     string? powerConnectors,
    [Required, Range(0, 10)] int outputHdmi,
    [Required, Range(0, 10)] int outputDp,
     int? cardLengthMm,
    [Required, Range(1, 5)] double cardSlots,
    [Required] ColorScheme colorScheme,
    [Required] Currency? Currency,
    [Range(0, 100000)] decimal? Price
);
