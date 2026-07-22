using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Repositories.Decorators;

public class CachedPsuRepository(
    PsuRepository inner,
    IMemoryListCache cache) : IPsuRepository
{
    public Task AddPsuAsync(PsuEntity psu, CancellationToken cancellationToken)=>
        inner.AddPsuAsync(psu, cancellationToken);

    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken)=>
        inner.BrandExistsAsync(brandId, cancellationToken);

    public Task DeletePsuAsync(PsuEntity psu, CancellationToken cancellationToken)=>
        inner.DeletePsuAsync(psu, cancellationToken);

    public Task<List<PsuEntity>> GetAllPsusAsync(CancellationToken cancellationToken)=>
        cache.GetOrCreateAsync(ComponentCacheKeys.PsusKeys, inner.GetAllPsusAsync, cancellationToken);

    public Task<PsuEntity?> GetPsuByIdAsync(int id, CancellationToken cancellationToken)=>
        inner.GetPsuByIdAsync(id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken)=>
        inner.SaveChangesAsync(cancellationToken);
}
