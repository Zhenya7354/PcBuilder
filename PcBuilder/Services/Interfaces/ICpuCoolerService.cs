using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface ICpuCoolerService
{
    public Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync();
    public Task<CpuCoolerEntity> GetCpuCoolerByIdAsync(int id);
    public Task<CpuCoolerEntity> AddCpuCoolerAsync(CpuCoolerCreateRequest dto);
    public Task<CpuCoolerEntity> UpdateCpuCoolerAsync(int id, CpuCoolerUpdateRequest dto);
    public Task DeleteCpuCoolerAsync(int id);
}
