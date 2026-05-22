using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Dtos;

public record BrandCreateDto([Required] string Name);
