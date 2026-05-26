using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface ICpuCoolerService
{
    public Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync();
    public Task<CpuCoolerEntity> GetCpuCoolerByIdAsync(int id);
    public Task<CpuCoolerEntity> AddCpuCoolerAsync(CpuCoolerCreate dto);
    public Task<CpuCoolerEntity> UpdateCpuCoolerAsync(int id, CpuCoolerUpdate dto);
    public Task DeleteCpuCoolerAsync(int id);
}
