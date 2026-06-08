using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IHardDriveRepository
{
    public Task<List<HardDriveEntity>> GetAllHardDrivesAsync(CancellationToken cancellationToken);
    public Task<HardDriveEntity?> GetHardDriveByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddHardDriveAsync(HardDriveEntity hardDrive, CancellationToken cancellationToken);
    public Task DeleteHardDriveAsync(HardDriveEntity hardDrive, CancellationToken cancellationToken);
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
