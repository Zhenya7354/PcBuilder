using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IMotherboardRepository
{
    public Task<List<MotherboardEntity>> GetAllMotherboardsAsync();
    public Task<MotherboardEntity?> GetMotherboardByIdAsync(int id);
    public Task AddMotherboard(MotherboardEntity motherboard);
    public Task DeleteMotherboard(MotherboardEntity motherboard);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
