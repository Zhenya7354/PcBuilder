using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos;

public record GpuUpdateDto(
    string? Name,
    int? BrandId,
    string? gpuChip,
    GpuInterface? gpuInterface,
     int? vram_gb,
    string? vramType,
     int? baseClockMhz,
    int? boostClockMhz,
    int? memoryBusBits,
     double? memoryBandwithGb,
    int? tdpWatts,
    int? recommendedPsuWattage,
     string? powerConnectors,
    int? outputHdmi,
    int? outputDp,
     int? cardLengthMm,
    double? cardSlots,
    bool? hasRgb,
    decimal? price

    );


