using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface ICpuService
{
    public Task<List<CpuEntity>> GetAllCpuAsync(CancellationToken cancellationToken);
    public Task<CpuEntity> GetCpuByIdAsync(int id, CancellationToken cancellationToken);
    public Task<CpuEntity> AddCpuAsync(CpuCreateRequest cpuDto, CancellationToken cancellationToken);
    public Task<CpuEntity> UpdateCpuAsync(int id, CpuUpdateRequest cpuDto, CancellationToken cancellationToken);
    public Task DeleteCpuAsync(int id, CancellationToken cancellationToken);
}
