using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PcBuilder.Configurations;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class MemoryListCache(
    IMemoryCache _cache,
    IOptions<CacheOptions> _options) : IMemoryListCache
{
    public async Task<List<T>> GetOrCreateAsync<T>(string cacheKey, Func<CancellationToken, Task<List<T>>> loadFunction, CancellationToken cancellationToken)
    {
        var result = await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.Value?.AbsoluteExpirationInMinutes ?? 0);
            entry.SlidingExpiration = TimeSpan.FromMinutes(_options.Value?.SlidingExpirationInMinutes ?? 0);
            entry.Size = _options.Value?.EntrySize ?? 1;

            var data = await loadFunction(cancellationToken);

            return data?.ToList() ?? new List<T>();
        });

        return result ?? new List<T>();
    }
}
