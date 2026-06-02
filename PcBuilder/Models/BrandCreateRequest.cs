using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record BrandCreateRequest([Required] string Name);
