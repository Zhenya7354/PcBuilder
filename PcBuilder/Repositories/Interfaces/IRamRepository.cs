using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IRamRepository
{
    public Task<List<RamEntity>> GetAllRamAsync();
    public Task<RamEntity?> GetRamByIdAsync(int id);
    public Task AddRam(RamEntity ram);
    public Task DeleteRam(RamEntity ram);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
