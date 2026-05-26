using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface ICpuService
{
    public Task<List<CpuEntity>> GetAllCpuAsync();
    public Task<CpuEntity> GetCpuByIdAsync(int id);
    public Task<CpuEntity> AddCpuAsync(CpuCreate cpuDto);
    public Task<CpuEntity> UpdateCpuAsync(int id, CpuUpdate cpuDto);
    public Task DeleteCpuAsync(int id);
}
