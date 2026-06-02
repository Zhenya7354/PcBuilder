using PcBuilder.Enums;

namespace PcBuilder.Models;

public record BuildComponentRequest(
    BuildComponentType ComponentType,
    int? ComponentId
    );
   
