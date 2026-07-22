using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Repositories.Decorators;

public class CachedMotherboardRepository(
    MotherboardRepository inner,
    IMemoryListCache cache) : IMotherboardRepository
{
    public Task AddMotherboardAsync(MotherboardEntity motherboard, CancellationToken cancellationToken)=>
        inner.AddMotherboardAsync(motherboard, cancellationToken);

    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken)=>
        inner.BrandExistsAsync(brandId, cancellationToken);

    public Task DeleteMotherboardAsync(MotherboardEntity motherboard, CancellationToken cancellationToken)=>
        inner.DeleteMotherboardAsync(motherboard, cancellationToken);

    public Task<List<MotherboardEntity>> GetAllMotherboardsAsync(CancellationToken cancellationToken)=>
        cache.GetOrCreateAsync(ComponentCacheKeys.MotherboardsKey, inner.GetAllMotherboardsAsync, cancellationToken);

    public Task<MotherboardEntity?> GetMotherboardByIdAsync(int id, CancellationToken cancellationToken)=>
        inner.GetMotherboardByIdAsync(id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken)=>
        inner.SaveChangesAsync(cancellationToken);
}
