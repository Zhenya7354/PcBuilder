using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Repositories.Decorators;

public class CachedGpuRepository(
    GpuRepository inner,
    IMemoryListCache cache) : IGpuRepository
{
    public Task AddGpuAsync(GpuEntity gpu, CancellationToken cancellationToken)=>
        inner.AddGpuAsync(gpu, cancellationToken);
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken)=>
        inner.BrandExistsAsync(brandId, cancellationToken);

    public Task DeleteGpuAsync(GpuEntity gpu, CancellationToken cancellationToken)=>
        inner.DeleteGpuAsync(gpu,cancellationToken);

    public Task<List<GpuEntity>> GetAllGpusAsync(CancellationToken cancellationToken)=>
        cache.GetOrCreateAsync(ComponentCacheKeys.GpusKey, inner.GetAllGpusAsync, cancellationToken);

    public Task<GpuEntity?> GetGpuByIdAsync(int id, CancellationToken cancellationToken)=>
        inner.GetGpuByIdAsync(id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken)=>
        inner.SaveChangesAsync(cancellationToken);
}
