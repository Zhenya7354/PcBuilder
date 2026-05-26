using PcBuilder.Entities;
using PcBuilder.Models;

namespace PcBuilder.Services.Interfaces;

public interface IGpuService
{
    public Task<List<GpuEntity>> GetGpusAsync();
    public Task<GpuEntity> GetGpuById(int id);
    public Task<GpuEntity> AddGpuAsync(GpuCreate gpuDto);
    public Task<GpuEntity> UpdateGpuAsync(int id, GpuUpdate gpuDto);
    public Task DeleteGpuAsync(int id);
}
