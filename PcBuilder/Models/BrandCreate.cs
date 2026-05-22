using System.ComponentModel.DataAnnotations;

namespace PcBuilder.Models;

public record BrandCreate([Required] string Name);
