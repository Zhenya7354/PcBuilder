using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Repositories.Interfaces;

public interface ICpuRepository
{
    public Task<List<CpuEntity>> GetAllCpusAsync();
    public Task<CpuEntity?> GetCpuByIdAsync(int id);
    public Task AddCpuAsync(CpuEntity cpu);
    public Task DeleteCpuAsync(CpuEntity cpu);
    public Task<bool> BrandExistsAsync(int brandId);
    public Task SaveChangesAsync();
}
