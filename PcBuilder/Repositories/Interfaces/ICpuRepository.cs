using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Repositories.Interfaces;

public interface ICpuRepository
{
    public Task<List<CpuEntity>> GetAllCpusAsync(CancellationToken cancellationToken);
    public Task<CpuEntity?> GetCpuByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddCpuAsync(CpuEntity cpu, CancellationToken cancellationToken);
    public Task DeleteCpuAsync(CpuEntity cpu, CancellationToken cancellationToken);
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
