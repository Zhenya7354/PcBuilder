namespace PcBuilder.Services.Interfaces;

public interface IMemoryListCache
{
    public Task<List<T>> GetOrCreateAsync<T>(string cacheKey, Func<CancellationToken, Task<List<T>>> loadMethod, CancellationToken cancellationToken);
}
