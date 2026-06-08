using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface ICpuCoolerRepository
{
    public Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync(CancellationToken cancellationToken);
    public Task<CpuCoolerEntity?> GetCpuCoolerByIdAsync(int id, CancellationToken cancellationToken);
    public Task AddCpuCoolerAsync(CpuCoolerEntity cpuCooler, CancellationToken cancellationToken);
    public Task DeleteCpuCoolerAsync(CpuCoolerEntity cpuCooler, CancellationToken cancellationToken);
    public Task<bool> BrandExistsAsync(int brandId, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
