using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Repositories.Decorators;

public class CachedRamRepository(
    RamRepository inner,
    IMemoryListCache cache) : IRamRepository
{
    public Task AddRamAsync(RamEntity ram, CancellationToken cancellationToken)=>
        inner.AddRamAsync(ram, cancellationToken);

    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken)=>
        inner.BrandExistsAsync(brandId, cancellationToken);

    public Task DeleteRamAsync(RamEntity ram, CancellationToken cancellationToken)=>
        inner.DeleteRamAsync(ram, cancellationToken);

    public Task<List<RamEntity>> GetAllRamAsync(CancellationToken cancellationToken)=>
        cache.GetOrCreateAsync(ComponentCacheKeys.RamsKey, inner.GetAllRamAsync, cancellationToken);

    public Task<RamEntity?> GetRamByIdAsync(int id, CancellationToken cancellationToken)=>
        inner.GetRamByIdAsync(id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken)=>
        inner.SaveChangesAsync(cancellationToken);
}
