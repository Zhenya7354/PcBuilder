using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IRamRepository
{
    public Task<List<RamEntity>> GetAllRamAsync();
    public Task<RamEntity?> GetRamByIdAsync(int id);
    public Task AddRamAsync(RamEntity ram);
    public Task DeleteRamAsync(RamEntity ram);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
