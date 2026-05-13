using PcBuilder.Enums;

namespace PcBuilder.Dtos
{
    public record BuildComponentDto(
        BuildComponentType ComponentType,
        int? ComponentId
        );
   
}
