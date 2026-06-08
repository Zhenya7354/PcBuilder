using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface IGpuRepository
{
    public Task<List<GpuEntity>> GetAllGpusAsync(CancellationToken cancellationToken);
    public Task<GpuEntity?> GetGpuByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddGpuAsync(GpuEntity gpu, CancellationToken cancellationToken);
    public Task DeleteGpuAsync(GpuEntity gpu, CancellationToken cancellationToken);
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);

}
