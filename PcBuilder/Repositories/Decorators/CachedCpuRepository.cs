using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Repositories.Decorators;

public class CachedCpuRepository(
    CpuRepository inner,
    IMemoryListCache cache) : ICpuRepository
{
    public Task AddCpuAsync(CpuEntity cpu, CancellationToken cancellationToken) =>
        inner.AddCpuAsync(cpu, cancellationToken);
    
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken) =>
        inner.BrandExistsAsync(brandId, cancellationToken);

    public Task DeleteCpuAsync(CpuEntity cpu, CancellationToken cancellationToken) =>
        inner.DeleteCpuAsync(cpu, cancellationToken);

    public Task<List<CpuEntity>> GetAllCpusAsync(CancellationToken cancellationToken) =>
        cache.GetOrCreateAsync(ComponentCacheKeys.CpusKey, inner.GetAllCpusAsync, cancellationToken);

    public Task<CpuEntity?> GetCpuByIdAsync(int id, CancellationToken cancellationToken) =>
        inner.GetCpuByIdAsync(id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        inner.SaveChangesAsync(cancellationToken);

}
