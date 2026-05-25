using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IHardDriveRepository
{
    public Task<List<HardDriveEntity>> GetAllHardDrivesAsync();
    public Task<HardDriveEntity?> GetHardDriveByIdAsync(int id);
    public Task AddHardDrive(HardDriveEntity hardDrive);
    public Task DeleteHardDrive(HardDriveEntity hardDrive);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
