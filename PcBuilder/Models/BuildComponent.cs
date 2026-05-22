using PcBuilder.Enums;

namespace PcBuilder.Models;

public record BuildComponent(
    BuildComponentType ComponentType,
    int? ComponentId
    );
   
