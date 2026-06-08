using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface ICpuCoolerService
{
    public Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync(CancellationToken cancellationToken);
    public Task<CpuCoolerEntity> GetCpuCoolerByIdAsync(int id, CancellationToken cancellationToken);
    public Task<CpuCoolerEntity> AddCpuCoolerAsync(CpuCoolerCreateRequest dto, CancellationToken cancellationToken);
    public Task<CpuCoolerEntity> UpdateCpuCoolerAsync(int id, CpuCoolerUpdateRequest dto, CancellationToken cancellationToken);
    public Task DeleteCpuCoolerAsync(int id, CancellationToken cancellationToken);
}
