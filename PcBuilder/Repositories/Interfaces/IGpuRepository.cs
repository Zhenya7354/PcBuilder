using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IGpuRepository
{
    public Task<List<GpuEntity>> GetAllGpusAsync();
    public Task<GpuEntity?> GetGpuByIdAsync(int id);
    public Task AddGpu(GpuEntity gpu);
    public Task DeleteGpu(GpuEntity gpu);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();

}
