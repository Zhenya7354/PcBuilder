using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Repositories.Decorators;

public class CachedHardDriveRepository(
    HardDriveRepository inner,
    IMemoryListCache cache) : IHardDriveRepository
{
    public Task AddHardDriveAsync(HardDriveEntity hardDrive, CancellationToken cancellationToken)=>
        inner.AddHardDriveAsync(hardDrive, cancellationToken);

    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken)=>
        inner.BrandExistsAsync(brandId, cancellationToken);

    public Task DeleteHardDriveAsync(HardDriveEntity hardDrive, CancellationToken cancellationToken)=>
        inner.DeleteHardDriveAsync(hardDrive, cancellationToken);

    public Task<List<HardDriveEntity>> GetAllHardDrivesAsync(CancellationToken cancellationToken)=>
        cache.GetOrCreateAsync(ComponentCacheKeys.HardDrivesKey, inner.GetAllHardDrivesAsync, cancellationToken);

    public Task<HardDriveEntity?> GetHardDriveByIdAsync(int id, CancellationToken cancellationToken)=>
        inner.GetHardDriveByIdAsync(id, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken)=>
        inner.SaveChangesAsync(cancellationToken);
}
