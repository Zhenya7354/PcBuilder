using PcBuilder.Enums;
using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos
{
    public record PsuCreateDto(
        [Required] string Name,
        [Required] int BrandId,
        [Required, Range(1, 3000)] int Wattage,
        [Required] PsuRating Efficiency,
        [Required] PsuModular Modularity,
        [Required] string AtxVersion,
        bool Has16Pin,
        [Required, Range(0, 10)] int EpsConnectors,
        [Required, Range(0, 30)] int SataConnectors,
        [Required, Range(0, 20)] int Pcie8PinConnectors,
        [Required, Range(40, 300)] int FanSizeMm,
        int? LengthMm,
        [Range(0, 100000)] decimal? PriceUsd
    );
}
