using PcBuilder.Enums;

namespace PcBuilder.Services.Interfaces;

public interface IComponentCatalogCacheInvalidator
{
    void InvalidateCache(BuildComponentType componentType);
    void InvalidateComponentCatalog();
}
