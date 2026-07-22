using Microsoft.Extensions.Caching.Memory;
using PcBuilder.Enums;
using PcBuilder.Mappers;
using PcBuilder.Models;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class ComponentCatalogCacheInvalidator(IMemoryCache _cache) : IComponentCatalogCacheInvalidator
{
    public void InvalidateComponentCatalog()
    {
        foreach (var key in ComponentCacheKeys.All)
        {
            _cache.Remove(key);
        }
    }

    public void InvalidateCache(BuildComponentType componentType)
    {
        var cacheKey = CacheKeyToBuildTypeMapper.GetCacheKeyForBuildComponentType(componentType);
        _cache.Remove(cacheKey);
    }
}
