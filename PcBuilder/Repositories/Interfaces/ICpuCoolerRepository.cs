using PcBuilder.Entities;

namespace PcBuilder.Repositories.Interfaces;

public interface ICpuCoolerRepository
{
    public Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync();
    public Task<CpuCoolerEntity?> GetCpuCoolerByIdAsync(int id);
    public Task AddCpuCoolerAsync(CpuCoolerEntity cpuCooler);
    public Task DeleteCpuCoolerAsync(CpuCoolerEntity cpuCooler);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
