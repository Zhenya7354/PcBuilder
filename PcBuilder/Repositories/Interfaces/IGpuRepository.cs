using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IGpuRepository
{
    public Task<List<GpuEntity>> GetAllGpusAsync();
    public Task<GpuEntity?> GetGpuByIdAsync(int id);
    public Task AddGpuAsync(GpuEntity gpu);
    public Task DeleteGpuAsync(GpuEntity gpu);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();

}
